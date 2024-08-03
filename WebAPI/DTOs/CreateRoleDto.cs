using System.Text.Json.Serialization;

namespace WebAPI.DTOs;

public sealed class CreateRoleDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    [JsonPropertyName("description")]
    public string Description { get; set; } = default!;
}
