/// <summary>
/// Represents the grammatical gender of a unit in different languages.
/// Some languages assign gender to units, which affects how they are used in sentences.
/// Based on CLDR (Common Locale Data Repository).
/// </summary>
public enum UnitGender
{
    /// <summary>
    /// Feminine gender.
    /// Example (French): "heure" (hour) is feminine.
    /// </summary>
    Feminine = 0,

    /// <summary>
    /// Masculine gender.
    /// Example (French): "kilomètre" (kilometer) is masculine.
    /// </summary>
    Masculine = 1,

    /// <summary>
    /// Inanimate (used when gender does not apply).
    /// Example: In English, most units are considered inanimate.
    /// </summary>
    Inanimate = 2,

    /// <summary>
    /// Neuter gender.
    /// Example (German): "Kilogramm" (kilogram) is neuter.
    /// </summary>
    Neuter = 3,

    /// <summary>
    /// Common gender (used in languages that do not distinguish between masculine and feminine).
    /// Example (Swedish): "meter" is common gender.
    /// </summary>
    Common = 4
}