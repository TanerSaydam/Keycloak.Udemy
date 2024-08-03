using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebAPI.DTOs;
using WebAPI.Options;
using WebAPI.Services;

namespace WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
//[Authorize]
public sealed class RolesController(
    KeycloakService keycloakService,
    IOptions<KeycloakConfiguration> options) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        string enpoint = $"{options.Value.HostName}/admin/realms/{options.Value.Realm}/clients/{options.Value.ClientUUID}/roles";

        var response = await keycloakService.GetAsync<List<RoleDto>>(enpoint, true, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetByName(string name, CancellationToken cancellationToken)
    {
        string enpoint = $"{options.Value.HostName}/admin/realms/{options.Value.Realm}/clients/{options.Value.ClientUUID}/roles/{name}";

        var response = await keycloakService.GetAsync<RoleDto>(enpoint, true, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRoleDto request, CancellationToken cancellationToken)
    {
        string enpoint = $"{options.Value.HostName}/admin/realms/{options.Value.Realm}/clients/{options.Value.ClientUUID}/roles";

        var response = await keycloakService.PostAsync<string>(enpoint, request, true, cancellationToken);

        if (response.IsSuccessful && response.Data is null)
        {
            response.Data = "Role create is successful";
        }

        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteByName(string name, CancellationToken cancellationToken)
    {
        string enpoint = $"{options.Value.HostName}/admin/realms/{options.Value.Realm}/clients/{options.Value.ClientUUID}/roles/{name}";

        var response = await keycloakService.DeleteAsync<string>(enpoint, true, cancellationToken);

        if (response.IsSuccessful && response.Data is null)
        {
            response.Data = "Role delete is successful";
        }

        return StatusCode(response.StatusCode, response);
    }
}
