using System.Text.Json.Serialization;

namespace Cubicfox.Domain.Common.Models;

public class ZenquotesRandom
{
    [JsonPropertyName("q")]
    public string Quotes { get; set; }
    [JsonPropertyName("a")]
    public string Author { get; set; }
    [JsonPropertyName("h")]
    public string hHeader { get; set; }
}
