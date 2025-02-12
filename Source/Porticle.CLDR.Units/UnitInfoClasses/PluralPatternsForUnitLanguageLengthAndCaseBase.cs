namespace Porticle.CLDR.Units.UnitInfoClasses
{
    internal abstract class PluralPatternsForUnitLanguageLengthAndCaseBase
    {
        /// <summary>
        /// Represents a base class for handling plurality-related string formatting across various counts.
        /// </summary>
        /// <remarks>
        /// This abstract class provides a structure for defining plural forms for numerical counts like
        /// "zero", "one", "two", "few", "many", and "other".
        /// </remarks>
        protected PluralPatternsForUnitLanguageLengthAndCaseBase(GrammaticalCase grammaticalCase)
        {
            GrammaticalCase = grammaticalCase;
        }

        public GrammaticalCase GrammaticalCase  { get; } 
        
        /// <summary>
        /// Other - Used for all cases that do not fit into specific plural rules.
        /// Example: "She has many apples."
        /// </summary>
        public string Other { get; internal set; }

        /// <summary>
        /// Zero - Used for cases where zero has a distinct grammatical form.
        /// Example: "There are 0 apples."
        /// </summary>
        public string? Zero { get; internal set; }

        /// <summary>
        /// One - Used for singular numbers (usually 1 in many languages).
        /// Example: "There is 1 apple."
        /// </summary>
        public string? One  { get; internal set; }

        /// <summary>
        /// Two - Used in languages that have a dual form (e.g., Arabic, Slovenian).
        /// Example: "There are 2 apples."
        /// </summary>
        public string? Two  { get; internal set; }

        /// <summary>
        /// Few - Used for small numbers, often 2-4, depending on the language.
        /// Example: "There are a few apples."
        /// </summary>
        public string? Few  { get; internal set; }

        /// <summary>
        /// Many - Used for larger quantities, often beyond a specific threshold.
        /// Example: "There are many apples."
        /// </summary>
        public string? Many { get; internal set; }

        public abstract string GetFormatByCount(int count);
    }
}