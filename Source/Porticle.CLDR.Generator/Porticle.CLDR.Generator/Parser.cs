using System.Globalization;
using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Text.RegularExpressions;
using Porticle.CLDR.Generator.Serialization;
using Porticle.CLDR.Units;

namespace Porticle.CLDR.Generator;

/// <summary>
/// Represents a parser responsible for processing CLDR unit data files and generating relevant output.
/// </summary>
public class Parser(string repositoryRoot)
{
    public void Run()
    {
        ImportUnits();
    }

    public string RepositoryRoot { get; set; } = repositoryRoot;

    private void ImportUnits()
    {
        var unitsFolder = Path.Combine(RepositoryRoot, "Data\\cldr-json\\cldr-json\\cldr-units-full\\main");

        var files = Directory.GetFiles(unitsFolder, "units.json", SearchOption.AllDirectories);

        foreach (var file in files)
        {
            ParseFile(file);
        }

        GenerateOutput();
    }

    private void GenerateOutput()
    {
        var unitToIndex = Patterns.Keys.Select(key => key.Unit).Distinct().OrderBy(s => s).Select((unit, index) => (unit, index)).ToDictionary(tuple => tuple.unit, tuple => tuple.index);

        var allUnitsOrdered = unitToIndex.Keys.OrderBy(s => s).ToList();


        var allUnitsPascal = allUnitsOrdered.Select(ToPascalCase).ToList();
        var duplicateGroupAfterPascalCase = allUnitsPascal.GroupBy(s => ToPascalCase(s)).FirstOrDefault(grouping => grouping.Count() > 1);

        if (duplicateGroupAfterPascalCase != null)
        {
            throw new ParsingException("Duplicate group " + string.Join(" ",duplicateGroupAfterPascalCase));
        }

        GenerateUnitEnumClass(allUnitsOrdered, unitToIndex);

        foreach (var unit in allUnitsOrdered)
        {
            var keyValuePairs = Patterns
                .Where(p => p.Key.Unit == unit)
                .OrderBy(p => p.Key.Language)
                .ThenBy(p => p.Key.PluralCategory)
                .ThenBy(p => p.Key.PluralFormLength)
                .ThenBy(p => p.Key.GrammaticalCase)
                .ToList();

            using MemoryStream ms = new MemoryStream();
            using DeflateStream ds = new DeflateStream(ms,CompressionLevel.Optimal);
            using BinaryWriter bw = new BinaryWriter(ds);
            
            bw.Write(keyValuePairs.Count);
            foreach (var pair in keyValuePairs)
            {
                var pattern = new PluralFormPatternInfo(pair.Key.Language, pair.Key.PluralFormLength, pair.Key.GrammaticalCase, pair.Key.PluralCategory, pair.Value);
                pattern.Serialize(bw);
            }

            var genderInfos = PatternExtraInfos
                .Where(pair => pair.Key.Unit == unit && pair.Key is { Length: PluralFormLength.Long, PatternExtraInfoType: PatternExtraInfoType.Gender })
                .OrderBy(pair => pair.Key.Language)
                .ToList();

            bw.Write(genderInfos.Count);
            foreach (var genderInfo in genderInfos)
            {
                // Unit has a GenderInfo per Language
                var unitGenderInfo = new UnitGenderInfo(genderInfo.Key.Language,UnitGenderMap[genderInfo.Value]);
                unitGenderInfo.Serialize(bw);
            }

            var extraInfos = PatternExtraInfos
                .Where(pair => pair.Key.Unit == unit && (pair.Key.PatternExtraInfoType == PatternExtraInfoType.DisplayName || pair.Key.PatternExtraInfoType == PatternExtraInfoType.PerUnitPattern))
                .GroupBy(pair => (pair.Key.Language, pair.Key.Length))
                .ToList();
            
            bw.Write(extraInfos.Count);
            foreach (var extraInfo in extraInfos)
            {
                string displayName = extraInfo.Where(pair => pair.Key.PatternExtraInfoType == PatternExtraInfoType.DisplayName).Select(pair => pair.Value).SingleOrDefault() ?? "";
                string perUnitPattern = extraInfo.Where(pair => pair.Key.PatternExtraInfoType == PatternExtraInfoType.PerUnitPattern).Select(pair => pair.Value).SingleOrDefault() ?? "";
                var unitExtraInfo = new UnitExtraInfo(extraInfo.Key.Language,extraInfo.Key.Length,displayName, perUnitPattern);
                unitExtraInfo.Serialize(bw);
            }
            bw.Flush();
            ds.Flush();
            ms.Flush();
            var array = ms.ToArray();
            File.WriteAllBytes(Path.Combine(RepositoryRoot,"Source/Porticle.CLDR.Generator/Porticle.CLDR.Units/Data",unitToIndex[unit]+".bin"), array);
            
            
            
        }
    }

    private void GenerateUnitEnumClass(List<string> allUnitsOrdered, Dictionary<string, int> unitToIndex)
    {
        List<EnumMember> unitEnumMembers = new List<EnumMember>();
        
        foreach (var unit in allUnitsOrdered)
        {
            var unitPascal = ToPascalCase(unit);
            var description = PatternExtraInfos.GetValueOrDefault(new PatternExtraInfoKey("en",PluralFormLength.Long,unit,PatternExtraInfoType.DisplayName));
            var enumComment = description == null ? unitPascal + " (" + unit + ")" : description + " (" + unit + ")"; 
            unitEnumMembers.Add(new EnumMember(unitPascal,unitToIndex[unit],enumComment));
        }

        var combine = Path.Combine(RepositoryRoot, "Source/Porticle.CLDR.Generator/Porticle.CLDR.Units/Unit.cs");
        File.WriteAllText(combine, CreateEnum("Porticle.CLDR.Units", "Unit", unitEnumMembers).ToString());
    }

    record EnumMember(string Name, int Number, string Remark);    
    
    StringBuilder CreateEnum(string namespaceName, string enumName, List<EnumMember> members)
    {
        // Generiere die Enum-Definition als String
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("// <autogenerated />");
        sb.AppendLine($"namespace {namespaceName}");
        sb.AppendLine("{");
        sb.AppendLine($"    public enum {enumName}");
        sb.AppendLine("    {");

        for (var index = 0; index < members.Count; index++)
        {
            var member = members[index];
            sb.AppendLine($"        /// <summary>");
            sb.AppendLine($"        /// {member.Remark}");
            sb.AppendLine($"        /// </summary>");
            sb.AppendLine($"        {member.Name} = {member.Number},");
            if (index < members.Count - 1)
            {
                sb.AppendLine();
            }
        }

        sb.AppendLine("    }");
        sb.AppendLine("}");

        return sb;
    }

    public static string ToPascalCase(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        // Entferne nicht-alphanumerische Zeichen und ersetze sie durch Leerzeichen
        input = Regex.Replace(input, @"[^a-zA-Z0-9]", " ");

        // Teile den String an Leerzeichen und wandle jedes Wort in Großbuchstaben um
        TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
        string pascalCase = textInfo.ToTitleCase(input.ToLower()).Replace(" ", "");

        return pascalCase;
    }
    
    private void ParseFile(string file)
    {
        Console.WriteLine("Parsing file "+file);
        var cldr = JsonSerializer.Deserialize<Cldr>(File.ReadAllText(file),new JsonSerializerOptions(){TypeInfoResolver = new DefaultJsonTypeInfoResolver()});

        foreach (var language in cldr.Main)
        {
            ParseLanguage(language.Key, language.Value);
        }
    }

    private void ParseLanguage(string language, Main main)
    {
        ParseUnits(language, PluralFormLength.Long, main.Units.Long);
        ParseUnits(language, PluralFormLength.Short, main.Units.Short);
        ParseUnits(language, PluralFormLength.Narrow, main.Units.Narrow);
    }

    private void ParseUnits(string language, PluralFormLength length, Dictionary<string, Dictionary<string, string>> units)
    {
        foreach (var unit in units)
        {
            ParseUnit(language, length, unit.Key, unit.Value);
        }
    }

    private void ParseUnit(string language, PluralFormLength length, string unit, Dictionary<string,string> unitPatterns)
    {
        var matchX = Regex.Match(unit,"^(?<result1>10|1024)p(?<result2>\\-?\\d+)$");
        if (matchX.Success)
        {
            // currently not implemented
            return;
        }
        if (unit is "per" or "power2" or "power3" or "times" or "coordinateUnit")
        {
            // currently not implemented
            return;
        }

        foreach (var unitValue in unitPatterns)
        {
            ParseUnitValue(language, length, unit, unitValue.Key, unitValue.Value);
        }
    }

    private Dictionary<string, GrammaticalCase> GrammaticalCaseMap { get; } = new()
    {
        { "unitPattern", GrammaticalCase.None },
        { "accusative", GrammaticalCase.Accusative },
        { "dative", GrammaticalCase.Dative },
        { "genitive", GrammaticalCase.Genitive },
        { "instrumental", GrammaticalCase.Instrumental },
        { "locative", GrammaticalCase.Locative },
        { "elative", GrammaticalCase.Elative },
        { "illative", GrammaticalCase.Illative },
        { "partitive", GrammaticalCase.Partitive },
        { "oblique", GrammaticalCase.Oblique },
        { "terminative", GrammaticalCase.Terminative },
        { "translative", GrammaticalCase.Translative },
        { "ablative", GrammaticalCase.Ablative },
        { "sociative", GrammaticalCase.Sociative },
        { "ergative", GrammaticalCase.Ergative },
        { "vocative", GrammaticalCase.Vocative },
        { "prepositional", GrammaticalCase.Prepositional },
    };
    private Dictionary<string, PluralCategory> PluralCategoryMap { get; } = new()
    {
        { "other", PluralCategory.Other },
        { "zero", PluralCategory.Zero },
        { "one", PluralCategory.One },
        { "two", PluralCategory.Two },
        { "few", PluralCategory.Few },
        { "many", PluralCategory.Many },
    };

    private Dictionary<string, UnitGender> UnitGenderMap { get; } = new()
    {
        { "feminine", UnitGender.Feminine },
        { "masculine", UnitGender.Masculine },
        { "inanimate", UnitGender.Inanimate },
        { "neuter", UnitGender.Neuter },
        { "common", UnitGender.Common },
    };
    
    private void ParseUnitValue(string language, PluralFormLength length, string unit, string unitPatternName, string unitPatternText)
    {
        if (unitPatternName == "displayName")
        {
            StorePatternExtraInfo(language, length, unit, PatternExtraInfoType.DisplayName  , unitPatternText);
            return;
        }
        
        if (unitPatternName == "gender")
        {
            if (UnitGenderMap.ContainsKey(unitPatternText))
            {
                StorePatternExtraInfo(language, length, unit, PatternExtraInfoType.Gender  , unitPatternText);
            }
            else
            {
                throw new ParsingException("cant parse unit gender '"+unitPatternText+"'");
            }
            return;
        }
        
        if (unitPatternName == "perUnitPattern")
        {
            StorePatternExtraInfo(language, length, unit, PatternExtraInfoType.PerUnitPattern  , unitPatternText);
            return;
        }
        
        var caseExpression = string.Join("|", GrammaticalCaseMap.Keys.Select(Regex.Escape));
        var categoryExpression = string.Join("|", PluralCategoryMap.Keys.Select(Regex.Escape));
        
        var match = Regex.Match(unitPatternName, $"^(?<case>({caseExpression}))-count-(?<category>({categoryExpression}))$",RegexOptions.ExplicitCapture);
        if (!match.Success)
        {
            throw new ParsingException("cant parse unit pattern name '"+unitPatternName+"'");
        }

        var grammaticalCase = GrammaticalCaseMap[match.Groups["case"].Value];
        var pluralCategory = PluralCategoryMap[match.Groups["category"].Value];

        StorePattern(language, length, unit, grammaticalCase, pluralCategory, unitPatternText);
    }

    private Dictionary<PatternKey, string> Patterns = new(); 
    private Dictionary<PatternExtraInfoKey, string> PatternExtraInfos = new(); 
    private void StorePatternExtraInfo(string language, PluralFormLength length, string unit, PatternExtraInfoType extraInfoType, string unitPatternText)
    {
        PatternExtraInfos.Add(new PatternExtraInfoKey(language, length, unit, extraInfoType), unitPatternText);
    }

    private void StorePattern(string language, PluralFormLength length, string unit, GrammaticalCase grammaticalCase, PluralCategory pluralCategory, string unitPatternText)
    {
        Patterns.Add(new PatternKey(language, length, unit, grammaticalCase, pluralCategory), unitPatternText);
    }
    
    record PatternExtraInfoKey(string Language, PluralFormLength Length, string Unit, PatternExtraInfoType PatternExtraInfoType);

    record PatternKey(string Language, PluralFormLength PluralFormLength, string Unit, GrammaticalCase GrammaticalCase, PluralCategory PluralCategory);
}