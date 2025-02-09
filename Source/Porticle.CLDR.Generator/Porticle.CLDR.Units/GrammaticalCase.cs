namespace Porticle.CLDR.Units
{
    /// <summary>
    /// Enumeration of grammatical cases as used in various languages for use in CLDR.
    /// </summary>
    public enum GrammaticalCase : byte
    {
        /// <summary>
        /// Represents the absence of a grammatical case or an undefined case.
        /// </summary>
        None,

        /// <summary>
        /// Accusative case - marks the direct object of a verb.
        /// Example: "She reads the book." ("the book" is in the accusative case)
        /// </summary>
        Accusative,

        /// <summary>
        /// Dative case - marks the indirect object of a verb.
        /// Example: "He gave her a gift." ("her" is in the dative case)
        /// </summary>
        Dative,

        /// <summary>
        /// Genitive case - indicates possession or relationship.
        /// Example: "This is John's car." ("John's" is in the genitive case)
        /// </summary>
        Genitive,

        /// <summary>
        /// Instrumental case - indicates the means or instrument used.
        /// Example: "He wrote with a pen." ("with a pen" is in the instrumental case)
        /// </summary>
        Instrumental,

        /// <summary>
        /// Locative case - denotes a location or place.
        /// Example: "She is in the house." ("in the house" is in the locative case)
        /// </summary>
        Locative,

        /// <summary>
        /// Elative case - indicates movement out of or from something.
        /// Example: "He came out of the building." ("out of the building" is in the elative case)
        /// </summary>
        Elative,

        /// <summary>
        /// Illative case - indicates movement into something.
        /// Example: "She went into the room." ("into the room" is in the illative case)
        /// </summary>
        Illative,

        /// <summary>
        /// Partitive case - denotes an indefinite or partial quantity.
        /// Example: "He drank some water." ("some water" is in the partitive case)
        /// </summary>
        Partitive,

        /// <summary>
        /// Oblique case - a general term for non-nominative cases.
        /// Used when a specific case is not determined.
        /// </summary>
        Oblique,

        /// <summary>
        /// Terminative case - indicates an endpoint or limit of an action.
        /// Example: "He worked until midnight." ("until midnight" is in the terminative case)
        /// </summary>
        Terminative,

        /// <summary>
        /// Translative case - denotes a change of state or transformation.
        /// Example: "She became a teacher." ("a teacher" is in the translative case)
        /// </summary>
        Translative,

        /// <summary>
        /// Ablative case - indicates movement away from something.
        /// Example: "He moved away from the city." ("from the city" is in the ablative case)
        /// </summary>
        Ablative,

        /// <summary>
        /// Sociative case - denotes accompaniment or association.
        /// Example: "She went with her friend." ("with her friend" is in the sociative case)
        /// </summary>
        Sociative,

        /// <summary>
        /// Ergative case - marks the subject of a transitive verb in ergative languages.
        /// Example: "The door opened." ("The door" is in the ergative case in some languages)
        /// </summary>
        Ergative,

        /// <summary>
        /// Vocative case - used for direct address.
        /// Example: "O Lord, hear my prayer!" ("O Lord" is in the vocative case)
        /// </summary>
        Vocative,

        /// <summary>
        /// Prepositional case - used exclusively with certain prepositions.
        /// Example: "He thinks about the problem." ("about the problem" is in the prepositional case)
        /// </summary>
        Prepositional
    }
}