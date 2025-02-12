using System.Diagnostics;
using System.Runtime.CompilerServices;
using Porticle.CLDR.Units.Serialization;
using Porticle.CLDR.Units.UnitInfoClasses;

namespace Porticle.CLDR.Units.Tests;

[TestClass]
public class UnitTests
{
    [TestMethod]
    public void TestMethod1()
    {
        var loader = new CldrResourceLoader();
 
        
        string result;
        
        
        
        
        PatternsForUnit weekPluralizer = loader.Load(Unit.DurationWeek);
        
        result = weekPluralizer.GetFormat("de", 3, PluralFormLength.Long, GrammaticalCase.Accusative); // --> "{0} Wochen"
        Assert.AreEqual(result, "{0} Wochen");
        
        result = weekPluralizer.GetFormat("de", 1, PluralFormLength.Long, GrammaticalCase.Accusative); // --> "{0} Wochen"
        Assert.AreEqual(result, "{0} Woche");
    }

    [TestMethod]
    public void TestMethod2()
    {
        var deserializer = new CldrResourceLoader();
        var x = new List<Unit>() { Unit.DurationWeek, Unit.DurationWeek, Unit.DurationWeek, Unit.AngleDegree, Unit.AngleRadian };
        
        var pluralizer = deserializer.Load(Unit.DurationWeek);
        
        Check(pluralizer);
    }    

    [TestMethod]
    public void TestAll()
    {
        HashSet<string> allLanguages = new HashSet<string>();
        foreach (Unit unit in Enum.GetValues<Unit>())
        {
            var cldrUnits = new CldrUnits(unit);
            foreach (var la in cldrUnits.GetAllSupportedLanguages())
            {
                allLanguages.Add(la);
            }
        }

        foreach (Unit unit in Enum.GetValues<Unit>())
        {
            if (unit is Unit.AreaBuJp or Unit.AreaCho or Unit.DurationDayPerson)
            {
                continue;
            }
            
            var cldrUnits = new CldrUnits(unit);

            // if (cldrUnits.GetAllSupportedLanguages().Length < allLanguages.Count)
            // {
            //     if (cldrUnits.GetAllSupportedLanguages().Contains("en"))
            //     {
            //         Console.WriteLine(unit+" is supported by "+cldrUnits.GetAllSupportedLanguages().Length +" / "+allLanguages.Count+" languages but fallback to en is supported");                
            //     }
            //     else if (cldrUnits.GetAllSupportedLanguages().Length == 1)
            //     {
            //         Console.WriteLine(unit+" is supported only by "+cldrUnits.GetAllSupportedLanguages().Single());                
            //     }
            //     else
            //     {
            //         Console.WriteLine(unit+" is supported by "+cldrUnits.GetAllSupportedLanguages().Length +" / "+allLanguages.Count+" languages without fallbach to en");                
            //     }
            // }

            foreach (var lang in cldrUnits.GetAllSupportedLanguages())
            {
                cldrUnits.GetUnitGender(lang);
                
                cldrUnits.GetFormatString(lang,2,PluralFormLength.Long,GrammaticalCase.Dative);
                cldrUnits.GetFormatString(lang,2,PluralFormLength.Short,GrammaticalCase.Dative);
                cldrUnits.GetFormatString(lang,2,PluralFormLength.Narrow,GrammaticalCase.Dative);
            }
            
            // foreach (var language in allLanguages)
            // {
            //     cldrUnits.GetUnitGender(language);
            // }
        }
    }    
    

    private static void Check(PatternsForUnit p)
    {
        foreach (var pair in p.PluralPatternsForUnitByLanguage)
        {
            Check(pair.Value.Long);
            Check(pair.Value.Short);
            Check(pair.Value.Narrow);
        }
    }

    private static void Check(PluralPatternsForUnitLanguageAndLength value)
    {
        Assert.IsNotNull(value);
        
            
        Assert.IsNotNull(value.None);
            
        Check(value.None);
    }

    private static void Check(PluralPatternsForUnitLanguageLengthAndCaseBase? valueNone)
    {
        Assert.IsNotNull(valueNone.Other);
    }
}