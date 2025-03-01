namespace Porticle.CLDR.Units
{
    /// <summary>
    ///     Defines the available length forms for pluralization based on CLDR Units.
    /// </summary>
    public enum PluralFormLength : byte
    {
        /// <summary>
        ///     The full, spelled-out form of the unit.
        ///     Example: "kilometers"
        /// </summary>
        Long = 0,

        /// <summary>
        ///     A shorter, commonly used abbreviation of the unit.
        ///     Example: "km"
        /// </summary>
        Short = 1,

        /// <summary>
        ///     A highly compact, often single-character representation.
        ///     Example: "㎞" (Unicode character for kilometers)
        /// </summary>
        Narrow = 2
    }
}