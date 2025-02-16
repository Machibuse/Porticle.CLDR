﻿using System.Collections.Generic;
using System.Linq;

namespace Porticle.CLDR.Units.UnitInfoClasses
{
    /// <summary>
    /// Represents a mapping of language-specific plural patterns and grammatical case information for a specific Unit.
    /// This means, this class contains all existing information for a specific unit.
    /// </summary>
    internal class PatternsForUnit
    {
        private const string FallbackLanguage = "en";
        
        public PatternsForUnit()
        {
        }


        internal Dictionary<string, PluralPatternsForUnitAndLanguage> PluralPatternsForUnitByLanguage { get; } = new Dictionary<string, PluralPatternsForUnitAndLanguage>();

        private PluralPatternsForUnitAndLanguage GetCaseInfoByLanguage(string language)
        {
            if (PluralPatternsForUnitByLanguage.TryGetValue(language, out var info))
            {
                return info;
            }

            if (language.Contains('-'))
            {
                if (PluralPatternsForUnitByLanguage.TryGetValue(language.Split('-')[0], out info))
                {
                    return info;
                }
            }
            
            if (PluralPatternsForUnitByLanguage.TryGetValue(FallbackLanguage, out info))
            {
                return info;
            }

            throw new CldrException("Language " + language + " is not supportet ");
        }

        public string GetFormat(string language, int count, PluralFormLength length, GrammaticalCase grammaticalCase)
        {
            var x = GetCaseInfoByLanguage(language);
            var pattern = x.GetByLength(length);

            var pluralPatternsForUnitLanguageLengthAndCaseBase = pattern.GetCountInfo(grammaticalCase);
            
            return pluralPatternsForUnitLanguageLengthAndCaseBase.GetFormatByCount(count);
        }

        public UnitGender GetUnitGender(string language)
        {
            var x = GetCaseInfoByLanguage(language);
            return x.Gender ?? UnitGender.Unknown;
        }
        
        public string[] GetAllSupportedLanguages()
        {
            return PluralPatternsForUnitByLanguage.Keys.ToArray();
        }
    }
}