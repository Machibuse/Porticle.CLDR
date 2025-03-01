using System;

namespace Porticle.CLDR.Units.UnitInfoClasses
{
    /// <summary>
    ///     Represents a collection of plural patterns for a specific unit and language.
    ///     Provides access to patterns for different plural form lengths (Long, Short, Narrow)
    ///     and associated unit gender.
    /// </summary>
    internal class PluralPatternsForUnitAndLanguage
    {
        private PluralPatternsForUnitLanguageAndLength? Long { get; set; }
        private PluralPatternsForUnitLanguageAndLength? Short { get; set; }
        private PluralPatternsForUnitLanguageAndLength? Narrow { get; set; }
        internal UnitGender? Gender { get; set; }

        /// <para>
        ///     Retrieves an existing instance of `PluralPatternsForUnitLanguageAndLength` for the specified
        ///     `PluralFormLength`, or creates and adds a new, empty instance if it does not already exist.
        /// </para>
        /// <param name="piPluralFormLength">
        ///     The plural form length for which the corresponding instance of
        ///     `PluralPatternsForUnitLanguageAndLength` is requested. This determines whether the
        ///     long, short, or narrow form is to be retrieved or added.
        /// </param>
        /// <returns>
        ///     An instance of `PluralPatternsForUnitLanguageAndLength` corresponding to the specified
        ///     `PluralFormLength`.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown if an invalid `PluralFormLength` is provided.
        /// </exception>
        /// <remarks>
        ///     For internal use when loading data from resources.
        /// </remarks>
        internal PluralPatternsForUnitLanguageAndLength GetOrAdd(PluralFormLength piPluralFormLength)
        {
            return piPluralFormLength switch
            {
                PluralFormLength.Long => Long ??= new PluralPatternsForUnitLanguageAndLength(),
                PluralFormLength.Short => Short ??= new PluralPatternsForUnitLanguageAndLength(),
                PluralFormLength.Narrow => Narrow ??= new PluralPatternsForUnitLanguageAndLength(),
                _ => throw new ArgumentOutOfRangeException(nameof(piPluralFormLength), piPluralFormLength, null)
            };
        }

        /// <summary>
        ///     Retrieves the instance of `PluralPatternsForUnitLanguageAndLength` corresponding to the specified
        ///     `PluralFormLength`. If the requested instance is null, it attempts to return instances
        ///     from other lengths in the order of availability.
        /// </summary>
        /// <param name="length">
        ///     The plural form length specifying whether to retrieve the long, short, or narrow instance.
        /// </param>
        /// <returns>
        ///     An instance of `PluralPatternsForUnitLanguageAndLength` corresponding to the specified `PluralFormLength`,
        ///     or another available instance if the requested one is null.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown if the provided `PluralFormLength` is not valid.
        /// </exception>
        /// <remarks>
        ///     Provided fallbacks are as follows:
        ///     Long -> Short -> Narrow,
        ///     Short -> Narrow -> Long,
        ///     Narrow -> Short -> Long
        /// </remarks>
        public PluralPatternsForUnitLanguageAndLength? GetPatternsByLength(PluralFormLength length)
        {
            switch (length)
            {
                case PluralFormLength.Long:
                    return Long ?? Short ?? Narrow;
                case PluralFormLength.Short:
                    return Short ?? Narrow ?? Long;
                case PluralFormLength.Narrow:
                    return Narrow ?? Short ?? Long;
                default:
                    throw new ArgumentOutOfRangeException(nameof(length), length, null);
            }
        }
    }
}