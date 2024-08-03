using Microsoft.Extensions.Options;
using System.Text.Json;
using WebAPI.DTOs;
using WebAPI.Options;

namespace WebAPI.Services;

public sealed class KeycloakService(
    IOptions<KeycloakConfiguration> options)
{
    public async Task<string> GetAccessToken(CancellationToken cancellationToken = default)
    {
        HttpClient client = new();

        string enpoint = $"{options.Value.HostName}/realms/{options.Value.Realm}/protocol/openid-connect/token";

        List<KeyValuePair<string, string>> data = new();
        KeyValuePair<string, string> grantType = new("grant_type", "client_credentials");
        KeyValuePair<string, string> clientId = new("client_id", options.Value.ClientId);
        KeyValuePair<string, string> clientSecret = new("client_secret", options.Value.ClientSecret);

        data.Add(grantType);
        data.Add(clientId);
        data.Add(clientSecret);

        var message = await client.PostAsync(enpoint, new FormUrlEncodedContent(data), cancellationToken);

        var response = await message.Content.ReadAsStringAsync();

        if (!message.IsSuccessStatusCode)
        {

            if (message.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var errorResultForBadRequest = JsonSerializer.Deserialize<BadRequestErrorResponseDto>(response);

                throw new ArgumentException(errorResultForBadRequest!.ErrorMessage);
            }

            var errorResultForOther = JsonSerializer.Deserialize<BadRequestErrorResponseDto>(response);
            throw new ArgumentException(errorResultForOther!.ErrorMessage);
        }

        var result = JsonSerializer.Deserialize<GetAccessTokenResponseDto>(response);

        return result!.AccessToken;
    }
}
