using System;

namespace Porticle.CLDR.Units.UnitInfoClasses
{
    /// <summary>
    ///     Plural Patterns for a Specific Unit, FormLength and Language
    /// </summary>
    /// <remarks>
    ///     This class organizes plural patterns according to specific grammatical cases such as accusative, dative, genitive, etc.,
    ///     along with a default case referred to as "None." It provides functionality to retrieve plural patterns for a given
    ///     grammatical case. These patterns can be useful in language-specific localization where grammatical cases and their
    ///     associated plural forms are necessary for proper linguistic representation.
    /// </remarks>
    internal class PluralPatternsForUnitLanguageAndLength
    {
        public string? DisplayName { get; internal set; }

        public string? PerUnitPattern { get; internal set; }

        private PluralPatternsForUnitLanguageLengthAndCaseBase? None { get; set; }

        private PluralPatternsForUnitLanguageLengthAndCaseBase? Accusative { get; set; }

        private PluralPatternsForUnitLanguageLengthAndCaseBase? Dative { get; set; }

        private PluralPatternsForUnitLanguageLengthAndCaseBase? Genitive { get; set; }

        private PluralPatternsForUnitLanguageLengthAndCaseBase? Instrumental { get; set; }

        private PluralPatternsForUnitLanguageLengthAndCaseBase? Locative { get; set; }

        private PluralPatternsForUnitLanguageLengthAndCaseBase? Elative { get; set; }

        private PluralPatternsForUnitLanguageLengthAndCaseBase? Illative { get; set; }

        private PluralPatternsForUnitLanguageLengthAndCaseBase? Partitive { get; set; }

        private PluralPatternsForUnitLanguageLengthAndCaseBase? Oblique { get; set; }

        private PluralPatternsForUnitLanguageLengthAndCaseBase? Terminative { get; set; }

        private PluralPatternsForUnitLanguageLengthAndCaseBase? Translative { get; set; }

        private PluralPatternsForUnitLanguageLengthAndCaseBase? Ablative { get; set; }

        private PluralPatternsForUnitLanguageLengthAndCaseBase? Sociative { get; set; }

        private PluralPatternsForUnitLanguageLengthAndCaseBase? Ergative { get; set; }

        private PluralPatternsForUnitLanguageLengthAndCaseBase? Vocative { get; set; }

        private PluralPatternsForUnitLanguageLengthAndCaseBase? Prepositional { get; set; }

        internal PluralPatternsForUnitLanguageLengthAndCaseBase GetOrAdd(GrammaticalCase grammaticalCase)
        {
            return grammaticalCase switch
            {
                GrammaticalCase.None => None ??= new PluralPatternsForUnitLanguageLengthAndCase(),
                GrammaticalCase.Accusative => Accusative ??= new PluralPatternsForUnitLanguageLengthAndCase(),
                GrammaticalCase.Dative => Dative ??= new PluralPatternsForUnitLanguageLengthAndCase(),
                GrammaticalCase.Genitive => Genitive ??= new PluralPatternsForUnitLanguageLengthAndCase(),
                GrammaticalCase.Instrumental => Instrumental ??= new PluralPatternsForUnitLanguageLengthAndCase(),
                GrammaticalCase.Locative => Locative ??= new PluralPatternsForUnitLanguageLengthAndCase(),
                GrammaticalCase.Elative => Elative ??= new PluralPatternsForUnitLanguageLengthAndCase(),
                GrammaticalCase.Illative => Illative ??= new PluralPatternsForUnitLanguageLengthAndCase(),
                GrammaticalCase.Partitive => Partitive ??= new PluralPatternsForUnitLanguageLengthAndCase(),
                GrammaticalCase.Oblique => Oblique ??= new PluralPatternsForUnitLanguageLengthAndCase(),
                GrammaticalCase.Terminative => Terminative ??= new PluralPatternsForUnitLanguageLengthAndCase(),
                GrammaticalCase.Translative => Translative ??= new PluralPatternsForUnitLanguageLengthAndCase(),
                GrammaticalCase.Ablative => Ablative ??= new PluralPatternsForUnitLanguageLengthAndCase(),
                GrammaticalCase.Sociative => Sociative ??= new PluralPatternsForUnitLanguageLengthAndCase(),
                GrammaticalCase.Ergative => Ergative ??= new PluralPatternsForUnitLanguageLengthAndCase(),
                GrammaticalCase.Vocative => Vocative ??= new PluralPatternsForUnitLanguageLengthAndCase(),
                GrammaticalCase.Prepositional => Prepositional ??= new PluralPatternsForUnitLanguageLengthAndCase(),
                _ => throw new ArgumentOutOfRangeException(nameof(grammaticalCase), grammaticalCase, null)
            };
        }


        /// <para>
        ///     Returns the plural patterns for the specified grammatical case, resolving to the most appropriate case if the specific case is not available.
        /// </para>
        /// <param name="grammaticalCase">The grammatical case to retrieve plural patterns for.</param>
        /// <returns>The plural patterns corresponding to the specified grammatical case, or a fallback case if unavailable.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when an unsupported grammatical case is provided.</exception>
        public PluralPatternsForUnitLanguageLengthAndCaseBase? GetCountInfo(GrammaticalCase grammaticalCase)
        {
            switch (grammaticalCase)
            {
                case GrammaticalCase.None:
                    return None ?? Oblique;
                case GrammaticalCase.Accusative:
                    return Accusative ?? None ?? Oblique;
                case GrammaticalCase.Dative:
                    return Dative ?? None ?? Oblique;
                case GrammaticalCase.Genitive:
                    return Genitive ?? None ?? Oblique;
                case GrammaticalCase.Instrumental:
                    return Instrumental ?? None ?? Oblique;
                case GrammaticalCase.Locative:
                    return Locative ?? None ?? Oblique;
                case GrammaticalCase.Elative:
                    return Elative ?? None ?? Oblique;
                case GrammaticalCase.Illative:
                    return Illative ?? None ?? Oblique;
                case GrammaticalCase.Partitive:
                    return Partitive ?? None ?? Oblique;
                case GrammaticalCase.Oblique:
                    return Oblique ?? None ?? Oblique;
                case GrammaticalCase.Terminative:
                    return Terminative ?? None ?? Oblique;
                case GrammaticalCase.Translative:
                    return Translative ?? None ?? Oblique;
                case GrammaticalCase.Ablative:
                    return Ablative ?? None ?? Oblique;
                case GrammaticalCase.Sociative:
                    return Sociative ?? None ?? Oblique;
                case GrammaticalCase.Ergative:
                    return Ergative ?? None ?? Oblique;
                case GrammaticalCase.Vocative:
                    return Vocative ?? None ?? Oblique;
                case GrammaticalCase.Prepositional:
                    return Prepositional ?? None ?? Oblique;
                default:
                    throw new ArgumentOutOfRangeException(nameof(grammaticalCase), grammaticalCase, null);
            }
        }
    }
}