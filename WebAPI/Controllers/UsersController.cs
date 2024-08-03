using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebAPI.DTOs;
using WebAPI.Options;
using WebAPI.Services;

namespace WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public sealed class UsersController(
    KeycloakService keycloakService,
    IOptions<KeycloakConfiguration> options) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        string enpoint = $"{options.Value.HostName}/admin/realms/{options.Value.Realm}/users";

        var response = await keycloakService.GetAsync<List<UserDto>>(enpoint, true, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
}
