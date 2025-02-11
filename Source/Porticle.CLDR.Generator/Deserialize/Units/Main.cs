using System.Text.Json.Serialization;

/// <summary>
/// Represents the main structure for CLDR (Common Locale Data Repository) unit data.
/// This class serves as the root object that contains identity metadata and unit formatting information.
/// </summary>
public partial class Main
{
    /// <summary>
    /// Contains metadata about the dataset, such as language, region, and version information.
    /// Example:
    /// {
    ///   "language": "en",
    ///   "script": "Latn",
    ///   "territory": "US"
    /// }
    /// </summary>
    [JsonPropertyName("identity")]
    public Identity Identity { get; set; }

    /// <summary>
    /// Contains unit formatting data, including long, short, and narrow representations,
    /// as well as specialized duration unit formats.
    /// Example:
    /// {
    ///   "long": { "length-meter": { "one": "meter", "other": "meters" } },
    ///   "short": { "length-meter": { "one": "m", "other": "m" } },
    ///   "narrow": { "length-meter": { "one": "m", "other": "m" } }
    /// }
    /// </summary>
    [JsonPropertyName("units")]
    public Units Units { get; set; }
}