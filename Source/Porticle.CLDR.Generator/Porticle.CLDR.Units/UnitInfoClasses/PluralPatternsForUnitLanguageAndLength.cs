using System;
using System.Collections.Specialized;
using System.IO;

namespace Porticle.CLDR.Units.UnitInfoClasses
{
    /// <summary>
    /// Plural Patterns for a Specific Unit, FormLength and Language
    /// </summary>
    /// <remarks>
    /// This class organizes plural patterns according to specific grammatical cases such as accusative, dative, genitive, etc.,
    /// along with a default case referred to as "None." It provides functionality to retrieve plural patterns for a given
    /// grammatical case. These patterns can be useful in language-specific localization where grammatical cases and their
    /// associated plural forms are necessary for proper linguistic representation.
    /// </remarks>
    public class PluralPatternsForUnitLanguageAndLength
    {
        public PluralPatternsForUnitLanguageAndLength()
        {
        }

        public string? DisplayName { get; internal set; }

        public string? PerUnitPattern { get; internal set; }

        internal PluralPatternsForUnitLanguageLengthAndCaseBase GetOrAdd(GrammaticalCase grammaticalCase)
        {
            switch (grammaticalCase)
            {
                case GrammaticalCase.None:
                    return None ??= new PluralPatternsForUnitLanguageLengthAndCase(grammaticalCase);
                case GrammaticalCase.Accusative:
                    return Accusative  ??= new PluralPatternsForUnitLanguageLengthAndCase(grammaticalCase);;
                case GrammaticalCase.Dative:
                    return Dative  ??= new PluralPatternsForUnitLanguageLengthAndCase(grammaticalCase);;
                case GrammaticalCase.Genitive:
                    return Genitive ??= new PluralPatternsForUnitLanguageLengthAndCase(grammaticalCase);;
                case GrammaticalCase.Instrumental:
                    return Instrumental ??= new PluralPatternsForUnitLanguageLengthAndCase(grammaticalCase);;
                case GrammaticalCase.Locative:
                    return Locative ??= new PluralPatternsForUnitLanguageLengthAndCase(grammaticalCase);;
                case GrammaticalCase.Elative:
                    return Elative  ??= new PluralPatternsForUnitLanguageLengthAndCase(grammaticalCase);;
                case GrammaticalCase.Illative:
                    return Illative  ??= new PluralPatternsForUnitLanguageLengthAndCase(grammaticalCase);;
                case GrammaticalCase.Partitive:
                    return Partitive  ??= new PluralPatternsForUnitLanguageLengthAndCase(grammaticalCase);;
                case GrammaticalCase.Oblique:
                    return Oblique  ??= new PluralPatternsForUnitLanguageLengthAndCase(grammaticalCase);;
                case GrammaticalCase.Terminative:
                    return Terminative  ??= new PluralPatternsForUnitLanguageLengthAndCase(grammaticalCase);;
                case GrammaticalCase.Translative:
                    return Translative  ??= new PluralPatternsForUnitLanguageLengthAndCase(grammaticalCase);;
                case GrammaticalCase.Ablative:
                    return Ablative  ??= new PluralPatternsForUnitLanguageLengthAndCase(grammaticalCase);;
                case GrammaticalCase.Sociative:
                    return Sociative  ??= new PluralPatternsForUnitLanguageLengthAndCase(grammaticalCase);;
                case GrammaticalCase.Ergative:
                    return Ergative  ??= new PluralPatternsForUnitLanguageLengthAndCase(grammaticalCase);;
                case GrammaticalCase.Vocative:
                    return Vocative ??= new PluralPatternsForUnitLanguageLengthAndCase(grammaticalCase);;
                case GrammaticalCase.Prepositional:
                    return Prepositional  ??= new PluralPatternsForUnitLanguageLengthAndCase(grammaticalCase);
                default:
                    throw new ArgumentOutOfRangeException(nameof(grammaticalCase), grammaticalCase, null);
            }
        }
                

        public PluralPatternsForUnitLanguageLengthAndCaseBase GetCountInfo(GrammaticalCase grammaticalCase)
        {
            switch (grammaticalCase)
            {
                case GrammaticalCase.None:
                    return None;
                case GrammaticalCase.Accusative:
                    return Accusative ?? None;
                case GrammaticalCase.Dative:
                    return Dative ?? None;
                case GrammaticalCase.Genitive:
                    return Genitive ?? None;
                case GrammaticalCase.Instrumental:
                    return Instrumental ?? None;
                case GrammaticalCase.Locative:
                    return Locative ?? None;
                case GrammaticalCase.Elative:
                    return Elative ?? None;
                case GrammaticalCase.Illative:
                    return Illative ?? None;
                case GrammaticalCase.Partitive:
                    return Partitive ?? None;
                case GrammaticalCase.Oblique:
                    return Oblique ?? None;
                case GrammaticalCase.Terminative:
                    return Terminative ?? None;
                case GrammaticalCase.Translative:
                    return Translative ?? None;
                case GrammaticalCase.Ablative:
                    return Ablative ?? None;
                case GrammaticalCase.Sociative:
                    return Sociative ?? None;
                case GrammaticalCase.Ergative:
                    return Ergative ?? None;
                case GrammaticalCase.Vocative:
                    return Vocative ?? None;
                case GrammaticalCase.Prepositional:
                    return Prepositional ?? None;
                default:
                    throw new ArgumentOutOfRangeException(nameof(grammaticalCase), grammaticalCase, null);
            }
        }

        public UnitGender? Gender { get; internal set;}

        /// <summary>
        /// Represents the absence of a grammatical case or an undefined case.
        /// </summary>
        public PluralPatternsForUnitLanguageLengthAndCaseBase None { get; internal set; }

        /// <summary>
        /// Accusative case - marks the direct object of a verb.
        /// Example: "She reads the book." ("the book" is in the accusative case)
        /// </summary>
        public PluralPatternsForUnitLanguageLengthAndCaseBase? Accusative { get; internal set; }

        /// <summary>
        /// Dative case - marks the indirect object of a verb.
        /// Example: "He gave her a gift." ("her" is in the dative case)
        /// </summary>
        public PluralPatternsForUnitLanguageLengthAndCaseBase? Dative { get; internal set;}

        /// <summary>
        /// Genitive case - indicates possession or relationship.
        /// Example: "This is John's car." ("John's" is in the genitive case)
        /// </summary>
        public PluralPatternsForUnitLanguageLengthAndCaseBase? Genitive { get; internal set; }

        /// <summary>
        /// Instrumental case - indicates the means or instrument used.
        /// Example: "He wrote with a pen." ("with a pen" is in the instrumental case)
        /// </summary>
        public PluralPatternsForUnitLanguageLengthAndCaseBase? Instrumental { get; internal set; }

        /// <summary>
        /// Locative case - denotes a location or place.
        /// Example: "She is in the house." ("in the house" is in the locative case)
        /// </summary>
        public PluralPatternsForUnitLanguageLengthAndCaseBase? Locative { get; internal set; }

        /// <summary>
        /// Elative case - indicates movement out of or from something.
        /// Example: "He came out of the building." ("out of the building" is in the elative case)
        /// </summary>
        public PluralPatternsForUnitLanguageLengthAndCaseBase? Elative { get; internal set; }

        /// <summary>
        /// Illative case - indicates movement into something.
        /// Example: "She went into the room." ("into the room" is in the illative case)
        /// </summary>
        public PluralPatternsForUnitLanguageLengthAndCaseBase? Illative{ get;  internal set;}

        /// <summary>
        /// Partitive case - denotes an indefinite or partial quantity.
        /// Example: "He drank some water." ("some water" is in the partitive case)
        /// </summary>
        public PluralPatternsForUnitLanguageLengthAndCaseBase? Partitive{ get; internal set; }

        /// <summary>
        /// Oblique case - a general term for non-nominative cases.
        /// Used when a specific case is not determined.
        /// </summary>
        public PluralPatternsForUnitLanguageLengthAndCaseBase? Oblique{ get; internal set; }

        /// <summary>
        /// Terminative case - indicates an endpoint or limit of an action.
        /// Example: "He worked until midnight." ("until midnight" is in the terminative case)
        /// </summary>
        public PluralPatternsForUnitLanguageLengthAndCaseBase? Terminative{ get; internal set; }

        /// <summary>
        /// Translative case - denotes a change of state or transformation.
        /// Example: "She became a teacher." ("a teacher" is in the translative case)
        /// </summary>
        public PluralPatternsForUnitLanguageLengthAndCaseBase? Translative{ get; internal set; }

        /// <summary>
        /// Ablative case - indicates movement away from something.
        /// Example: "He moved away from the city." ("from the city" is in the ablative case)
        /// </summary>
        public PluralPatternsForUnitLanguageLengthAndCaseBase? Ablative{ get; internal set; }

        /// <summary>
        /// Sociative case - denotes accompaniment or association.
        /// Example: "She went with her friend." ("with her friend" is in the sociative case)
        /// </summary>
        public PluralPatternsForUnitLanguageLengthAndCaseBase? Sociative{ get; internal set; }

        /// <summary>
        /// Ergative case - marks the subject of a transitive verb in ergative languages.
        /// Example: "The door opened." ("The door" is in the ergative case in some languages)
        /// </summary>
        public PluralPatternsForUnitLanguageLengthAndCaseBase? Ergative{ get; internal set; }

        /// <summary>
        /// Vocative case - used for direct address.
        /// Example: "O Lord, hear my prayer!" ("O Lord" is in the vocative case)
        /// </summary>
        public PluralPatternsForUnitLanguageLengthAndCaseBase? Vocative{ get; internal set; }

        /// <summary>
        /// Prepositional case - used exclusively with certain prepositions.
        /// Example: "He thinks about the problem." ("about the problem" is in the prepositional case)
        /// </summary>
        public PluralPatternsForUnitLanguageLengthAndCaseBase? Prepositional{ get; internal set; }
    }
}