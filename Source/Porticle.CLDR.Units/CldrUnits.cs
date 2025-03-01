using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using Porticle.CLDR.Units.UnitInfoClasses;

namespace Porticle.CLDR.Units
{
    /// <summary>
    ///     Provides functionality for accessing and retrieving CLDR unit patterns and related information.
    /// </summary>
    /// <remarks>
    ///     The CldrUnits class allows querying for CLDR-based unit patterns, unit genders, and supported languages.
    ///     Patterns and associated data are retrieved from a cache for efficient access.
    /// </remarks>
    public class CldrUnits
    {
        private readonly PatternsForUnit _patterns;
        private readonly Unit _unit;

        /// <summary>
        ///     Represents an instance that provides access to CLDR unit patterns retrieved from the cache.
        /// </summary>
        /// <param name="unit">The unit for which patterns are retrieved.</param>
        public CldrUnits(Unit unit)
        {
            _unit = unit;
            _patterns = UnitsCache.GetPatterns(unit);
        }

        /// <summary>
        ///     Gets a format string with the given settings
        /// </summary>
        /// <param name="language">The language code used to retrieve the appropriate format string.</param>
        /// <param name="count">The numeric value for which the format string is applied.</param>
        /// <param name="length">The desired presentation of the format string (Long, Short, or Narrow).</param>
        /// <param name="grammaticalCase">The grammatical case used in the format string, if applicable.</param>
        /// <remarks>
        ///     <para>Supportet only by 'ja':</para>
        ///     AreaBuJp, AreaCho, AreaSeJp, LengthJoJp, LengthKen, LengthRiJp, LengthRin, LengthShakuCloth,
        ///     LengthShakuLength, LengthSun, MassFun, VolumeCupJp, VolumeKoku, VolumeKosaji, VolumeOsaji,
        ///     VolumeSai, VolumeShaku, VolumeToJp
        ///     <para>Supportet only by some languages but with fallback to 'en':</para>
        ///     PressureGasolineEnergyDensity, EnergyFoodcalorie, GraphicsDot, GraphicsDotPerCentimeter, GraphicsDotPerInch
        ///     <para>Supportet only by some languages without fallback to 'en':</para>
        ///     DurationDayPerson
        /// </remarks>
        /// <returns>The format String like "{0} weeks"</returns>
        public string GetFormatString(string language, int count, PluralFormLength length, GrammaticalCase grammaticalCase)
        {
            return _patterns.GetFormat(language, count, length, grammaticalCase) ?? GetFallbackPattern(PluralFormLength.Long);
        }

        /// <summary>
        /// Retrieves the format string for a given culture, count, plural form length, and grammatical case.
        /// </summary>
        /// <param name="culture">The cultural information used to determine localization patterns.</param>
        /// <param name="count">The numeric value for determining the plural form of the format string.</param>
        /// <param name="length">The length of the plural form, such as Long, Short, or Narrow.</param>
        /// <param name="grammaticalCase">The grammatical case used to generate the format string.</param>
        /// <returns>A format string that matches the specified culture, count, plural form length, and grammatical case. If no specific format string is found, a fallback pattern is returned.</returns>
        public string GetFormatString(CultureInfo culture, int count, PluralFormLength length, GrammaticalCase grammaticalCase)
        {
            return _patterns.GetFormat(culture.Name, count, length, grammaticalCase) ?? GetFallbackPattern(PluralFormLength.Long);
        }

        /// <summary>
        /// Formats a unit description based on the specified culture, count, length, and grammatical case.
        /// </summary>
        /// <param name="culture">The culture information to be used for formatting the unit.</param>
        /// <param name="count">The numerical value of the unit to be formatted.</param>
        /// <param name="length">The desired length of the plural form (e.g., Long, Short, or Narrow).</param>
        /// <param name="grammaticalCase">The grammatical case used in the formatting, for supporting language-specific features.</param>
        /// <param name="numberFormat">Optional. A custom number format string for formatting the numeric value. Defaults to null.</param>
        /// <returns>A string that represents the formatted unit based on the provided parameters.</returns>
#if NET7_0_OR_GREATER
        public string FormatUnit(CultureInfo culture, int count, PluralFormLength length, GrammaticalCase grammaticalCase, [StringSyntax("NumericFormat")] string? numberFormat = null)
#else
        public string FormatUnit(CultureInfo culture, int count, PluralFormLength length, GrammaticalCase grammaticalCase, string? numberFormat = null)
#endif
        {
            if (numberFormat == null)
            {
                return string.Format(culture, _patterns.GetFormat(culture.Name, count, length, grammaticalCase) ?? GetFallbackPattern(PluralFormLength.Long), count);
            }

            return string.Format(culture, _patterns.GetFormat(culture.Name, count, length, grammaticalCase) ?? GetFallbackPattern(PluralFormLength.Long),
                count.ToString(numberFormat, culture));
        }

        /// <summary>
        /// Retrieves the display name of a unit for the specified language and plural form length.
        /// </summary>
        /// <param name="language">The language code for which the display name is retrieved.</param>
        /// <param name="length">The plural form length to use when retrieving the display name.</param>
        /// <returns>The display name of the unit as a string, or null if no display name is available for the given parameters.</returns>
        public string? GetDisplayName(string language, PluralFormLength length)
        {
            return _patterns.GetGetDisplayName(language, length);
        }

        /// <summary>
        /// Retrieves the display name for a unit as per the specified culture and plural form length.
        /// </summary>
        /// <param name="culture">The culture for which the display name is retrieved.</param>
        /// <param name="length">The plural form length (e.g., long, short, or narrow).</param>
        /// <returns>
        /// The display name of the unit for the given culture and length, or null if the information is unavailable.
        /// </returns>
        public string? GetDisplayName(CultureInfo culture, PluralFormLength length)
        {
            return _patterns.GetGetDisplayName(culture.Name, length);
        }

        /// <summary>
        /// Retrieves the "per unit" pattern for a specific language and plural form length.
        /// </summary>
        /// <param name="language">The language code to retrieve the per unit pattern for.</param>
        /// <param name="length">The plural form length to retrieve the pattern (e.g., Long, Short, Narrow).</param>
        /// <returns>The "per unit" pattern as a string, or null if no pattern is found for the specified parameters.</returns>
        public string? GetPerUnitPattern(string language, PluralFormLength length)
        {
            return _patterns.GetPerUnitPattern(language, length);
        }

        /// <summary>
        /// Retrieves the "per unit" pattern for the specified culture and plural form length.
        /// </summary>
        /// <param name="culture">The culture information that determines the language for retrieving the pattern.</param>
        /// <param name="length">The plural form length which specifies the desired format form (e.g., long, short, or narrow).</param>
        /// <returns>
        /// A string representing the "per unit" pattern for the specified culture and form length,
        /// or null if the pattern is not available.
        /// </returns>
        public string? GetPerUnitPattern(CultureInfo culture, PluralFormLength length)
        {
            return _patterns.GetPerUnitPattern(culture.Name, length);
        }

        /// <summary>
        ///     Retrieves the long format string for a specified language and count and Oblique grammatical case.
        /// </summary>
        /// <param name="language">The language code for which the format string is retrieved.</param>
        /// <param name="count">The numerical count that determines the plural form.</param>
        /// <returns>
        ///     A long format string specific to the provided language and count.
        /// </returns>
        public string GetFormatStringLong(string language, int count)
        {
            return _patterns.GetFormat(language, count, PluralFormLength.Long, GrammaticalCase.Oblique) ?? GetFallbackPattern(PluralFormLength.Long);
        }
        
        /// <summary>
        /// Retrieves the long format string for a specified culture and count value.
        /// </summary>
        /// <param name="culture">The <see cref="CultureInfo"/> representing the culture for which the format string is retrieved.</param>
        /// <param name="count">The count value used to determine the appropriate plural form.</param>
        /// <returns>A <see cref="string"/> representing the long format pattern. If no specific pattern is found, a fallback pattern is returned.</returns>
        public string GetFormatStringLong(CultureInfo culture, int count)
        {
            return _patterns.GetFormat(culture.Name, count, PluralFormLength.Long, GrammaticalCase.Oblique) ?? GetFallbackPattern(PluralFormLength.Long);
        }

        /// <summary>
        /// Formats a unit using the "Long" plural form length and a default grammatical case for the specified culture.
        /// </summary>
        /// <param name="culture">The culture information used for formatting the unit.</param>
        /// <param name="count">The numeric value associated with the unit.</param>
        /// <param name="numberFormat">An optional custom number format for the numeric value.</param>
        /// <returns>A formatted string representing the unit in the specified culture and format.</returns>
#if NET7_0_OR_GREATER
        public string FormatUnitLong(CultureInfo culture, int count, string? numberFormat = null)
#else
        public string FormatUnitLong(CultureInfo culture, int count, string? numberFormat = null)
#endif
        {
            return FormatUnit(culture, count, PluralFormLength.Long, GrammaticalCase.Oblique, numberFormat);
        }

        /// <summary>
        ///     Retrieves the fallback pattern for a specified plural form length.
        /// </summary>
        /// <param name="pluralFormLength">The plural form length for which the fallback pattern is needed.</param>
        /// <returns>A string representing the fallback pattern for the specified plural form length.</returns>
        private string GetFallbackPattern(PluralFormLength pluralFormLength)
        {
            return GetFallbackPattern(_unit, pluralFormLength);
        }

        /// <summary>
        ///     Retrieves the fallback pattern for a given unit and plural form length.
        /// </summary>
        /// <param name="unit">The unit for which the fallback pattern is retrieved.</param>
        /// <param name="length">The plural form length for the fallback pattern (Long, Short, Narrow).</param>
        /// <returns>
        ///     A string representing the fallback pattern for the specified unit and plural form length.
        /// </returns>
        private static string GetFallbackPattern(Unit unit, PluralFormLength length)
        {
            var field = typeof(Unit).GetField(unit.ToString());

            if (field != null)
            {
                var fallbackValues = field.GetCustomAttribute<UnitFallbackValuesAttribute>();

                if (fallbackValues != null)
                    return length switch
                    {
                        PluralFormLength.Long => fallbackValues.Long,
                        PluralFormLength.Short => fallbackValues.Short,
                        PluralFormLength.Narrow => fallbackValues.Narrow,
                        _ => throw new ArgumentOutOfRangeException(nameof(length), length, null)
                    };
            }

            return "{0} " + unit;
        }

        /// <summary>
        ///     Retrieves the grammatical gender for a specified unit in a given language.
        ///     This is used to determine the appropriate grammatical form to apply in localization contexts.
        /// </summary>
        /// <param name="language">The language code for which the unit's gender should be retrieved.</param>
        /// <returns>The grammatical gender of the unit as a <see cref="UnitGender" /> enumeration value.</returns>
        public UnitGender GetUnitGender(string language)
        {
            return _patterns.GetUnitGender(language) ?? UnitGender.Unknown;
        }

        /// <summary>
        ///     Retrieves the grammatical gender for a specified unit in a given culture.
        ///     This is used to determine the appropriate grammatical form to apply in localization contexts.
        /// </summary>
        /// <param name="culture">The culture for which the unit gender is retrieved.</param>
        /// <returns>The <see cref="UnitGender"/> that represents the grammatical gender of the unit for the specified culture. Returns <see cref="UnitGender.Unknown"/> if the gender is not defined.</returns>
        public UnitGender GetUnitGender(CultureInfo culture)
        {
            return _patterns.GetUnitGender(culture.Name) ?? UnitGender.Unknown;
        }
        
        /// <summary>
        ///     Retrieves a list of all languages supported by the current unit's patterns in the CLDR dataset.
        /// </summary>
        /// <returns>An array of strings representing the language codes for all supported languages.</returns>
        public string[] GetAllSupportedLanguages()
        {
            return _patterns.GetAllSupportedLanguages();
        }
    }
}