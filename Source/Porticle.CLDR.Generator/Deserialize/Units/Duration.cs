using System.Text.Json.Serialization;

namespace Porticle.CLDR.Generator.Deserialize.Units;

/// <summary>
///     Represents duration formatting patterns from the CLDR (Common Locale Data Repository).
///     This class defines how duration-related units (such as hours, minutes, and seconds)
///     are formatted in different locales.
/// </summary>
public class Duration
{
    /// <summary>
    ///     The standard duration unit pattern used for formatting time durations.
    ///     Example: "h:mm"
    /// </summary>
    [JsonPropertyName("durationUnitPattern")]
    public string? DurationUnitPattern { get; set; }

    /// <summary>
    ///     An alternative variant of the duration unit pattern,
    ///     which may be used in specific contexts or locales.
    ///     Example: "h:mm"
    /// </summary>
    [JsonPropertyName("durationUnitPattern-alt-variant")]
    public string? DurationUnitPatternAltVariant { get; set; }
}