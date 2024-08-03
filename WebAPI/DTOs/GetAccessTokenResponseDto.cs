using System.Text.Json.Serialization;

namespace WebAPI.DTOs;

public sealed class GetAccessTokenResponseDto
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = default!;
}
