using System.Text.Json.Serialization;

/// <summary>
/// Represents identity metadata from the CLDR (Common Locale Data Repository).
/// This class provides information about the language, script, region, and variant used in the dataset.
/// </summary>
public partial class Identity
{
    /// <summary>
    /// Specifies the language code based on the ISO 639 standard.
    /// Example: "en" for English, "de" for German.
    /// </summary>
    [JsonPropertyName("language")]
    public string Language { get; set; }

    /// <summary>
    /// Specifies the territory (country/region) using the ISO 3166-1 alpha-2 code.
    /// Example: "US" for United States, "DE" for Germany.
    /// This property is optional and will be ignored if null.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("territory")]
    public string Territory { get; set; }

    /// <summary>
    /// Specifies the script used in the language based on ISO 15924.
    /// Example: "Latn" for Latin, "Cyrl" for Cyrillic.
    /// This property is optional and will be ignored if null.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("script")]
    public string Script { get; set; }

    /// <summary>
    /// Specifies any language variant, such as regional dialects or special usage.
    /// Example: "1901" for the old German orthography.
    /// This property is optional and will be ignored if null.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("variant")]
    public string Variant { get; set; }
}