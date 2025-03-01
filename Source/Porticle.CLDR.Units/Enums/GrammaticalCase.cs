namespace Porticle.CLDR.Units
{
    /// <summary>
    ///     Enumeration of grammatical cases as used in various languages for use in CLDR.
    /// </summary>
    public enum GrammaticalCase : byte
    {
        /// <summary>
        ///     Represents the absence of a grammatical case or an undefined case.
        /// </summary>
        None,

        /// <summary>
        ///     Accusative case - marks the direct object of a verb.
        ///     Example: "I have lived here for five years." ("five years" is in the accusative case)
        /// </summary>
        Accusative,

        /// <summary>
        ///     Dative case - marks the indirect object of a verb.
        ///     Example: "He gave five years to the project." ("five years" is in the dative case)
        /// </summary>
        Dative,

        /// <summary>
        ///     Genitive case - indicates possession or relationship.
        ///     Example: "The country's five years of progress." ("five years" is in the genitive case)
        /// </summary>
        Genitive,

        /// <summary>
        ///     Instrumental case - indicates the means or instrument used.
        ///     Example: "He measured time with five years." ("five years" is in the instrumental case)
        /// </summary>
        Instrumental,

        /// <summary>
        ///     Locative case - denotes a location or place.
        ///     Example: "In five years, everything changed." ("five years" is in the locative case)
        /// </summary>
        Locative,

        /// <summary>
        ///     Elative case - indicates movement out of or from something.
        ///     Example: "He emerged out of five difficult years." ("five difficult years" is in the elative case)
        /// </summary>
        Elative,

        /// <summary>
        ///     Illative case - indicates movement into something.
        ///     Example: "He entered into five years of study." ("five years of study" is in the illative case)
        /// </summary>
        Illative,

        /// <summary>
        ///     Partitive case - denotes an indefinite or partial quantity.
        ///     Example: "He spent some of his five years abroad." ("five years" is in the partitive case)
        /// </summary>
        Partitive,

        /// <summary>
        ///     Oblique case - a general term for non-nominative cases.
        ///     Used when a specific case is not determined.
        /// </summary>
        Oblique,

        /// <summary>
        ///     Terminative case - indicates an endpoint or limit of an action.
        ///     Example: "He worked until five years passed." ("five years" is in the terminative case)
        /// </summary>
        Terminative,

        /// <summary>
        ///     Translative case - denotes a change of state or transformation.
        ///     Example: "Over five years, he became an expert." ("five years" is in the translative case)
        /// </summary>
        Translative,

        /// <summary>
        ///     Ablative case - indicates movement away from something.
        ///     Example: "He moved away after five years." ("five years" is in the ablative case)
        /// </summary>
        Ablative,

        /// <summary>
        ///     Sociative case - denotes accompaniment or association.
        ///     Example: "He worked together with five years of experience." ("five years of experience" is in the sociative case)
        /// </summary>
        Sociative,

        /// <summary>
        ///     Ergative case - marks the subject of a transitive verb in ergative languages.
        ///     Example: "Five years changed him completely." ("five years" is in the ergative case in some languages)
        /// </summary>
        Ergative,

        /// <summary>
        ///     Vocative case - used for direct address.
        ///     Example: "O five years, where have you gone?" ("five years" is in the vocative case)
        /// </summary>
        Vocative,

        /// <summary>
        ///     Prepositional case - used exclusively with certain prepositions.
        ///     Example: "He spoke about five years of struggle." ("five years of struggle" is in the prepositional case)
        /// </summary>
        Prepositional
    }
}