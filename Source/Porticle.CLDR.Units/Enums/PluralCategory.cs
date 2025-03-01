namespace Porticle.CLDR.Units
{
    /// <summary>
    ///     Enumeration of plural categories as defined by the CLDR project.
    /// </summary>
    public enum PluralCategory
    {
        /// <summary>
        ///     Other - Used for all cases that do not fit into specific plural rules.
        ///     Example: "She has many apples."
        /// </summary>
        Other = 0,

        /// <summary>
        ///     One - Used for singular numbers (usually 1 in many languages).
        ///     Example: "There is 1 apple."
        /// </summary>
        One = 1,

        /// <summary>
        ///     Zero - Used for cases where zero has a distinct grammatical form.
        ///     Example: "There are 0 apples."
        /// </summary>
        Zero = 2,

        /// <summary>
        ///     Two - Used in languages that have a dual form (e.g., Arabic, Slovenian).
        ///     Example: "There are 2 apples."
        /// </summary>
        Two = 3,

        /// <summary>
        ///     Few - Used for small numbers, often 2-4, depending on the language.
        ///     Example: "There are a few apples."
        /// </summary>
        Few = 4,

        /// <summary>
        ///     Many - Used for larger quantities, often beyond a specific threshold.
        ///     Example: "There are many apples."
        /// </summary>
        Many = 5
    }
}