using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the root structure for CLDR (Common Locale Data Repository) data.
/// This class serves as the top-level container for localized unit data and metadata.
/// </summary>
public class Cldr
{
    /// <summary>
    /// A dictionary containing locale-specific CLDR data.
    /// The key represents the locale (e.g., "en-US", "de-DE"), 
    /// and the value is a <see cref="Main"/> object that holds 
    /// identity metadata and unit formatting details for that locale.
    /// 
    /// Example:
    /// {
    ///   "en-US": { "identity": { "language": "en", "territory": "US" }, "units": { ... } },
    ///   "de-DE": { "identity": { "language": "de", "territory": "DE" }, "units": { ... } }
    /// }
    /// </summary>
    [JsonPropertyName("main")]
    public Dictionary<string, Main> Main { get; set; }
}