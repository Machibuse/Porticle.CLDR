using System;
using Porticle.CLDR.Units.UnitInfoClasses;

namespace Porticle.CLDR.Units
{
    /// <summary>
    /// Provides functionality for accessing and retrieving CLDR unit patterns and related information.
    /// </summary>
    /// <remarks>
    /// The CldrUnits class allows querying for CLDR-based unit patterns, unit genders, and supported languages.
    /// Patterns and associated data are retrieved from a cache for efficient access.
    /// </remarks>
    public class CldrUnits
    {
        private readonly PatternsForUnit _patterns;

        /// <summary>
        /// Represents an instance that provides access to CLDR unit patterns retrieved from the cache.
        /// </summary>
        /// <param name="unit">The unit for which patterns are retrieved.</param>
        public CldrUnits(Unit unit)
        {
            _patterns = UnitsCache.GetPatterns(unit);
        }

        /// <summary>
        /// Gets a format string with the given settings
        /// </summary>
        /// <param name="language">The language code used to retrieve the appropriate format string.</param>
        /// <param name="count">The numeric value for which the format string is applied.</param>
        /// <param name="length">The desired presentation of the format string (Long, Short, or Narrow).</param>
        /// <param name="grammaticalCase">The grammatical case used in the format string, if applicable.</param>
        /// <exception cref="CldrException">
        /// When the format string does not exist. This should only occure in some very special cases.
        /// Normally there is a Fallback to 'en' language when a Unit not exists in the given language.
        /// But for example there are some special unit that only exists in one language like 'ja'.  
        /// </exception>
        /// <remarks>
        /// <para>Supportet only by 'ja':</para>
        /// AreaBuJp, AreaCho, AreaSeJp, LengthJoJp, LengthKen, LengthRiJp, LengthRin, LengthShakuCloth,
        /// LengthShakuLength, LengthSun, MassFun, VolumeCupJp, VolumeKoku, VolumeKosaji, VolumeOsaji,
        /// VolumeSai, VolumeShaku, VolumeToJp
        /// <para>Supportet only by some languages but with fallback to 'en':</para>
        /// PressureGasolineEnergyDensity, EnergyFoodcalorie, GraphicsDot, GraphicsDotPerCentimeter, GraphicsDotPerInch
        /// <para>Supportet only by some languages without fallback to 'en':</para>
        /// DurationDayPerson
        /// </remarks>
        /// <returns>The format String like "{0} weeks"</returns>
        public string GetFormatString(string language, int count, PluralFormLength length, GrammaticalCase grammaticalCase)
        {
            return _patterns.GetFormat(language, count, length, grammaticalCase);
        }

        /// <summary>
        /// Retrieves a safe format string based on the provided language, count, length, and grammatical case settings.
        /// If the requested format is not supported, the method returns a default format string "{0}" without throwing exceptions.
        /// </summary>
        /// <param name="language">The language code used to retrieve the appropriate format string.</param>
        /// <param name="count">The numeric value for which the format string is applied.</param>
        /// <param name="length">The desired presentation of the format string (Long, Short, or Narrow).</param>
        /// <param name="grammaticalCase">The grammatical case used in the format string, if applicable.</param>
        /// <returns>A format string such as "{0} weeks" if supported, or the fallback "{0}" if not supported.</returns>
        public string GetFormatStringSafe(string language, int count, PluralFormLength length, GrammaticalCase grammaticalCase)
        {
            try
            {
                return _patterns.GetFormat(language, count, length, grammaticalCase);
            }
            catch
            {
                return "{0}";
            }
        }


        /// <summary>
        /// Retrieves the grammatical gender for a specified unit in a given language.
        /// This is used to determine the appropriate grammatical form to apply in localization contexts.
        /// </summary>
        /// <param name="language">The language code for which the unit's gender should be retrieved.</param>
        /// <returns>The grammatical gender of the unit as a <see cref="UnitGender"/> enumeration value.</returns>
        public UnitGender GetUnitGender(string language)
        {
            try
            {
                return _patterns.GetUnitGender(language);
            }
            catch
            {
                // Return unknown on incomplete data
                return UnitGender.Unknown;
            }
        }

        /// <summary>
        /// Retrieves a list of all languages supported by the current unit's patterns in the CLDR dataset.
        /// </summary>
        /// <returns>An array of strings representing the language codes for all supported languages.</returns>
        public string[] GetAllSupportedLanguages()
        {
            return _patterns.GetAllSupportedLanguages();
        }
    }
}