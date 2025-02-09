/// <summary>
/// Defines additional metadata related to unit patterns in CLDR (Common Locale Data Repository).
/// This enum is used to specify extra information about a unit, such as its display name, gender, or per-unit pattern.
/// </summary>
internal enum PatternExtraInfoType
{
    /// <summary>
    /// Represents the localized display name of the unit.
    /// Example (English): "meter"
    /// Example (French): "mètre"
    /// </summary>
    DisplayName,

    /// <summary>
    /// Represents the grammatical gender of the unit.
    /// Example (French): "kilomètre" is masculine, "heure" is feminine.
    /// </summary>
    Gender,

    /// <summary>
    /// Represents the pattern for "per unit" expressions.
    /// Example: "{0} per {1}" → "kilometers per hour"
    /// </summary>
    PerUnitPattern
}