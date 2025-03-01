using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Porticle.CLDR.Units.UnitInfoClasses;

namespace Porticle.CLDR.Units.Serialization
{
    public class CldrResourceLoader
    {
        internal PatternsForUnit Load(Unit unit)
        {
            var resourceName = "Porticle.CLDR.Units.Data." + unit.ToString("D") + ".bin";
            using (var ms = new MemoryStream())
            {
                // read and unpack complete resource to memorystream because of binary reader performance problem
                // this makes a performance boost of factor 300
                using (var manifestResourceStream = typeof(CldrResourceLoader).Assembly.GetManifestResourceStream(resourceName))
                {
                    if (manifestResourceStream == null) throw new FileNotFoundException("Embedded Resource " + resourceName + " not found");

                    using (var ds = new DeflateStream(manifestResourceStream, CompressionMode.Decompress))
                    {
                        ds.CopyTo(ms);
                        ms.Seek(0, SeekOrigin.Begin);
                    }
                }

                using (var br = new BinaryReader(ms))
                {
                    var pluralFormPatternCount = br.ReadInt32();
                    var pluralFormPatternInfos = new List<PluralFormPatternInfo>(pluralFormPatternCount);
                    for (var i = 0; i < pluralFormPatternCount; i++) pluralFormPatternInfos.Add(new PluralFormPatternInfo(br));

                    var genderInfosCount = br.ReadInt32();
                    var genderInfos = new List<UnitGenderInfo>(genderInfosCount);
                    for (var i = 0; i < genderInfosCount; i++) genderInfos.Add(new UnitGenderInfo(br));

                    var unitExtraInfosCount = br.ReadInt32();
                    var unitExtraInfos = new List<UnitExtraInfo>(unitExtraInfosCount);
                    for (var i = 0; i < unitExtraInfosCount; i++) unitExtraInfos.Add(new UnitExtraInfo(br));

                    return CreatePluralPatternsForCasesForLanguages(pluralFormPatternInfos, genderInfos, unitExtraInfos);
                }
            }
        }

        private PatternsForUnit CreatePluralPatternsForCasesForLanguages(List<PluralFormPatternInfo> pluralFormInfo, List<UnitGenderInfo> genderInfosInfos,
            List<UnitExtraInfo> unitExtraInfos)
        {
            var patterns = new PatternsForUnit();

            foreach (var pluralForms in pluralFormInfo)
            {
                if (!patterns.PluralPatternsForUnitByLanguage.TryGetValue(pluralForms.Language, out var pluralPattern))
                {
                    pluralPattern = new PluralPatternsForUnitAndLanguage();
                    patterns.PluralPatternsForUnitByLanguage.Add(pluralForms.Language, pluralPattern);
                }

                var pluralPatternsForUnitLanguageAndLength = pluralPattern.GetOrAdd(pluralForms.PluralFormLength);

                var pluralPatternsForUnitLanguageLengthAndCaseBase = pluralPatternsForUnitLanguageAndLength.GetOrAdd(pluralForms.GrammaticalCase);

                switch (pluralForms.PluralCategory)
                {
                    case PluralCategory.Other:
                        pluralPatternsForUnitLanguageLengthAndCaseBase.Other = pluralForms.Text;
                        break;
                    case PluralCategory.One:
                        pluralPatternsForUnitLanguageLengthAndCaseBase.One = pluralForms.Text;
                        break;
                    case PluralCategory.Zero:
                        pluralPatternsForUnitLanguageLengthAndCaseBase.Zero = pluralForms.Text;
                        break;
                    case PluralCategory.Two:
                        pluralPatternsForUnitLanguageLengthAndCaseBase.Two = pluralForms.Text;
                        break;
                    case PluralCategory.Few:
                        pluralPatternsForUnitLanguageLengthAndCaseBase.Few = pluralForms.Text;
                        break;
                    case PluralCategory.Many:
                        pluralPatternsForUnitLanguageLengthAndCaseBase.Many = pluralForms.Text;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            foreach (var unitExtraInfo in unitExtraInfos)
            {
                if (!patterns.PluralPatternsForUnitByLanguage.TryGetValue(unitExtraInfo.Language, out var pluralPatternsForUnitAndLanguage))
                {
                    pluralPatternsForUnitAndLanguage = new PluralPatternsForUnitAndLanguage();
                    patterns.PluralPatternsForUnitByLanguage.Add(unitExtraInfo.Language, pluralPatternsForUnitAndLanguage);
                }
                
                var pluralPatternsForUnitLanguageAndLength = pluralPatternsForUnitAndLanguage.GetOrAdd(unitExtraInfo.Length);

                pluralPatternsForUnitLanguageAndLength.DisplayName = unitExtraInfo.DisplayName;
                pluralPatternsForUnitLanguageAndLength.PerUnitPattern = unitExtraInfo.PerUnitPattern;
            }

            foreach (var genderInfo in genderInfosInfos)
            {
                if (!patterns.PluralPatternsForUnitByLanguage.TryGetValue(genderInfo.Language, out var pluralPatternsForUnitAndLanguage))
                {
                    pluralPatternsForUnitAndLanguage = new PluralPatternsForUnitAndLanguage();
                    patterns.PluralPatternsForUnitByLanguage.Add(genderInfo.Language, pluralPatternsForUnitAndLanguage);

                    pluralPatternsForUnitAndLanguage.Gender = genderInfo.UnitGender;
                }
            }

            return patterns;
        }
    }
}