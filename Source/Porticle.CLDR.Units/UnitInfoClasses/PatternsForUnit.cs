using System;
using System.Collections.Generic;
using System.Linq;

namespace Porticle.CLDR.Units.UnitInfoClasses
{
    /// <summary>
    ///     Represents a mapping of language-specific plural patterns and grammatical case information for a specific Unit.
    ///     This means, this class contains all existing information for a specific unit.
    /// </summary>
    internal class PatternsForUnit
    {
        private const string FallbackLanguage = "en";

        /// <summary>
        ///     A property that represents a dictionary mapping languages to their associated plural patterns
        ///     and grammatical case information for a specific unit. This enables language-specific formatting
        ///     and grammatical rules for the unit, adhering to CLDR (Common Locale Data Repository) standards.
        /// </summary>
        internal Dictionary<string, PluralPatternsForUnitAndLanguage> PluralPatternsForUnitByLanguage { get; } = new Dictionary<string, PluralPatternsForUnitAndLanguage>();

        /// <summary>
        ///     Retrieves the plural patterns and grammatical case information for a specific unit,
        ///     based on the given language code. If no exact match for the given language exists,
        ///     attempts are made to retrieve data by falling back to less specific language codes
        ///     or a default fallback language.
        /// </summary>
        /// <param name="language">The BCP-47 language code used to retrieve the associated case information.</param>
        /// <returns>
        ///     The <see cref="PluralPatternsForUnitAndLanguage" /> containing plural patterns
        ///     and grammatical case information for the specified language. Returns null if no matching
        ///     language information is found.
        /// </returns>
        private PluralPatternsForUnitAndLanguage? GetCaseInfoByLanguage(string language)
        {
            if (PluralPatternsForUnitByLanguage.TryGetValue(language, out var info)) return info;

            if (language.Contains('-'))
                if (PluralPatternsForUnitByLanguage.TryGetValue(language.Split('-')[0], out info))
                    return info;

            if (PluralPatternsForUnitByLanguage.TryGetValue(FallbackLanguage, out info)) return info;

            return null;
        }
        
        /// <summary>
        ///     Retrieves the appropriate format pattern for a unit, based on the specified language,
        ///     count, plural form length, and grammatical case. This method selects the most suitable
        ///     pattern by accessing the respective case, plural, and count information.
        /// </summary>
        /// <param name="language">The BCP-47 language code used to retrieve the associated format patterns.</param>
        /// <param name="count">The numeric value that represents the quantity of the unit, used to determine the plural form.</param>
        /// <param name="length">The desired length for the plural form pattern (e.g., Long, Short, Narrow).</param>
        /// <param name="grammaticalCase">The applicable grammatical case for the format (e.g., Nominative, Accusative).</param>
        /// <returns>
        ///     A string representing the appropriate format pattern for the specified language, count,
        ///     plural form length, and grammatical case. Returns null if no matching pattern is found.
        /// </returns>
        public string? GetFormat(string language, int count, PluralFormLength length, GrammaticalCase grammaticalCase)
        {
            var caseInfo = GetCaseInfoByLanguage(language);

            var patterns = caseInfo?.GetPatternsByLength(length);

            var countInfo = patterns?.GetCountInfo(grammaticalCase);

            return countInfo?.GetFormatByCount(count);
        }

        /// <summary>
        ///     Retrieves the display name for a specific unit based on the given language
        ///     and plural form length. If no exact match for the given language or length exists,
        ///     the method attempts to retrieve the best matching data available.
        /// </summary>
        /// <param name="language">The BCP-47 language code used to locate the display name.</param>
        /// <param name="length">The plural form length (e.g., long, short, narrow) used to determine the appropriate display name.</param>
        /// <returns>
        ///     A string representing the display name for the unit in the specified language and length,
        ///     or null if no matching data is found.
        /// </returns>
        public string? GetGetDisplayName(string language, PluralFormLength length)
        {
            var caseInfo = GetCaseInfoByLanguage(language);
            switch (length)
            {
                case PluralFormLength.Long:
                    return caseInfo?.GetPatternsByLength(PluralFormLength.Long)?.DisplayName
                           ?? caseInfo?.GetPatternsByLength(PluralFormLength.Short)?.DisplayName
                           ?? caseInfo?.GetPatternsByLength(PluralFormLength.Narrow)?.DisplayName;
                case PluralFormLength.Short:
                    return caseInfo?.GetPatternsByLength(PluralFormLength.Short)?.DisplayName
                           ?? caseInfo?.GetPatternsByLength(PluralFormLength.Narrow)?.DisplayName
                           ?? caseInfo?.GetPatternsByLength(PluralFormLength.Long)?.DisplayName;
                case PluralFormLength.Narrow:
                    return caseInfo?.GetPatternsByLength(PluralFormLength.Narrow)?.DisplayName
                           ?? caseInfo?.GetPatternsByLength(PluralFormLength.Short)?.DisplayName
                           ?? caseInfo?.GetPatternsByLength(PluralFormLength.Long)?.DisplayName;
                default:
                    throw new ArgumentOutOfRangeException(nameof(length), length, null);
            }
        }

        /// <summary>
        ///     Retrieves the "per-unit" formatting pattern for a specific unit, based on the provided language and
        ///     plural form length. The pattern describes how a unit should be expressed as a fraction or ratio of another unit.
        /// </summary>
        /// <param name="language">The BCP-47 language code used to find the associated "per-unit" pattern.</param>
        /// <param name="length">The plural form length (long, short, or narrow) used to determine the specific formatting pattern.</param>
        /// <returns>
        ///     A string representing the "per-unit" pattern for the specified language and length.
        ///     Returns null if no matching pattern is found for the given combination of language and length.
        /// </returns>
        public string? GetPerUnitPattern(string language, PluralFormLength length)
        {
            return GetCaseInfoByLanguage(language)?.GetPatternsByLength(length)?.PerUnitPattern;
        }

        /// <summary>
        ///     Retrieves the grammatical gender of a unit for a specified language.
        ///     If no gender information is available for the given language, null is returned.
        /// </summary>
        /// <param name="language">The BCP-47 language code used to retrieve the associated unit gender.</param>
        /// <returns>
        ///     The <see cref="UnitGender" /> representing the grammatical gender of the unit
        ///     for the specified language, or null if no gender information is available.
        /// </returns>
        public UnitGender? GetUnitGender(string language)
        {
            return GetCaseInfoByLanguage(language)?.Gender;
        }

        /// <summary>
        ///     Retrieves a list of all languages for which plural patterns and grammatical case information
        ///     are available for the unit.
        /// </summary>
        /// <returns>
        ///     An array of strings representing the language codes that have associated
        ///     plural patterns and grammatical case information.
        /// </returns>
        public string[] GetAllSupportedLanguages()
        {
            return PluralPatternsForUnitByLanguage.Keys.ToArray();
        }
    }
}