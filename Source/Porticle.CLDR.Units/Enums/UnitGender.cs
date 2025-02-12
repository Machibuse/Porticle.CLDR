/// <summary>
/// Defines grammatical genders for units to enable localized representation.
/// This enumeration provides options for representing gender distinctions such as feminine, masculine,
/// inanimate objects, neuter genders, common genders, and cases where gender is not specified or unknown.
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
    Common = 4,

    /// <summary>
    /// Represents an unspecified or unknown grammatical gender.
    /// This value is used when the gender of a unit is not defined or cannot be determined.
    /// </summary>
    Unknown = 5
}