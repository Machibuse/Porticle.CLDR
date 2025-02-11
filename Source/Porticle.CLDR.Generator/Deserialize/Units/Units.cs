using System.Text.Json.Serialization;

/// <summary>
/// Represents unit formatting data from the CLDR (Common Locale Data Repository).
/// This class provides localized unit representations in different styles and duration formats.
/// </summary>
public class Units
{
    /// <summary>
    /// Contains the full, spelled-out names of units for different locales.
    /// Example:
    /// {
    ///   "length-meter": { "one": "meter", "other": "meters" },
    ///   "mass-kilogram": { "one": "kilogram", "other": "kilograms" }
    /// }
    /// </summary>
    [JsonPropertyName("long")]
    public Dictionary<string, Dictionary<string, string>> Long { get; set; }

    /// <summary>
    /// Contains the abbreviated forms of units.
    /// Example:
    /// {
    ///   "length-meter": { "one": "m", "other": "m" },
    ///   "mass-kilogram": { "one": "kg", "other": "kg" }
    /// }
    /// </summary>
    [JsonPropertyName("short")]
    public Dictionary<string, Dictionary<string, string>> Short { get; set; }

    /// <summary>
    /// Contains the narrowest possible representation of units, typically without spacing.
    /// Example:
    /// {
    ///   "length-meter": { "one": "m", "other": "m" },
    ///   "mass-kilogram": { "one": "kg", "other": "kg" }
    /// }
    /// </summary>
    [JsonPropertyName("narrow")]
    public Dictionary<string, Dictionary<string, string>> Narrow { get; set; }

    /// <summary>
    /// Represents a duration unit formatted as hours and minutes (hm).
    /// Example: "1h 30m"
    /// </summary>
    [JsonPropertyName("durationUnit-type-hm")]
    public Duration DurationUnitTypeHm { get; set; }

    /// <summary>
    /// Represents a duration unit formatted as hours, minutes, and seconds (hms).
    /// Example: "1h 30m 15s"
    /// </summary>
    [JsonPropertyName("durationUnit-type-hms")]
    public Duration DurationUnitTypeHms { get; set; }

    /// <summary>
    /// Represents a duration unit formatted as minutes and seconds (ms).
    /// Example: "30m 15s"
    /// </summary>
    [JsonPropertyName("durationUnit-type-ms")]
    public Duration DurationUnitTypeMs { get; set; }
}