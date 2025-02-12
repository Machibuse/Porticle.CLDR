namespace Porticle.CLDR.Units.UnitInfoClasses
{
    /// <summary>
    /// Represents information for handling plural formats based on a given count.
    /// This implementation handles the default behavior for most languages without using few and many cases.
    /// Few and many cases are only used in a few languages and need to have special implementations.  
    /// </summary>
    internal class PluralPatternsForUnitLanguageLengthAndCase : PluralPatternsForUnitLanguageLengthAndCaseBase
    {
        public PluralPatternsForUnitLanguageLengthAndCase(GrammaticalCase grammaticalCase) : base(grammaticalCase)
        {
        }

        /// <summary>
        /// Returns the appropriate format string based on the specified count value.
        /// This method considers the count to determine whether to use the Zero, One,
        /// Two, or Other format string.
        /// </summary>
        /// <param name="count">The numeric value representing the count for which the format should be determined.</param>
        /// <returns>
        /// A string representing the format associated with the specified count.
        /// Returns the "Zero" value if the count is 0 and the "Zero" property is not null,
        /// the "One" value if the count is 1 and the "One" property is not null,
        /// the "Two" value if the count is 2, otherwise returns the "Other" value.
        /// </returns>
        public override string GetFormatByCount(int count)
        {
            if (count == 0)
            {
                return Zero ?? Other;
            }
            if (count == 1)
            {
                return One ?? Other;
            }

            if (count == 2)
            {
                return Two ?? Other;
            }

            return Other;
        }
    }
}