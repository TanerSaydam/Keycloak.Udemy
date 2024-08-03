using System.Text.Json.Serialization;

namespace WebAPI.DTOs;

public sealed class RoleDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
}
