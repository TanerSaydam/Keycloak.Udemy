using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebAPI.DTOs;
using WebAPI.Options;
using WebAPI.Services;

namespace WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public sealed class UserRolesController(
    KeycloakService keycloakService,
    IOptions<KeycloakConfiguration> options) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AssignmentRolesByUserId(Guid id, List<RoleDto> request, CancellationToken cancellationToken)
    {
        string enpoint = $"{options.Value.HostName}/admin/realms/{options.Value.Realm}/users/{id}/role-mappings/clients/{options.Value.ClientUUID}";

        var response = await keycloakService.PostAsync<string>(enpoint, request, true, cancellationToken);

        if (response.IsSuccessful && response.Data is null)
        {
            response.Data = "Roles assignments is successful";
        }

        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete]
    public async Task<IActionResult> UnassignmentRolesByUserId(Guid id, List<RoleDto> request, CancellationToken cancellationToken)
    {
        string enpoint = $"{options.Value.HostName}/admin/realms/{options.Value.Realm}/users/{id}/role-mappings/clients/{options.Value.ClientUUID}";

        var response = await keycloakService.DeleteAsync<string>(enpoint, request, true, cancellationToken);

        if (response.IsSuccessful && response.Data is null)
        {
            response.Data = "Roles unassignments is successful";
        }

        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUserRolesByUserId(Guid id, CancellationToken cancellationToken)
    {
        string enpoint = $"{options.Value.HostName}/admin/realms/{options.Value.Realm}/users/{id}/role-mappings/clients/{options.Value.ClientUUID}";

        var response = await keycloakService.GetAsync<object>(enpoint, true, cancellationToken);


        return StatusCode(response.StatusCode, response);
    }

}
