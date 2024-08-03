using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebAPI.DTOs;
using WebAPI.Options;
using WebAPI.Services;

namespace WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public sealed class AuthController(
    IOptions<KeycloakConfiguration> options,
    KeycloakService keycloakService
    ) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto request, CancellationToken cancellationToken = default)
    {
        string endpoint = $"{options.Value.HostName}/admin/realms/{options.Value.Realm}/users";
        object data = new
        {
            username = request.UserName,
            firstName = request.FirstName,
            lastName = request.LastName,
            email = request.Email,
            enabled = true,
            emailVerified = true,
            credentials = new List<object>
            {
                new
                {
                    type = "password",
                    temporary = false,
                    value = request.Password
                }
            }
        };


        var response = await keycloakService.PostAsync<string>(endpoint, data, true, cancellationToken);

        if (response.IsSuccessful && response.Data is null)
        {
            response.Data = "User create is successful";
        }

        return StatusCode(response.StatusCode, response);
    }
}
