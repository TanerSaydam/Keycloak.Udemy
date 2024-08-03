﻿using Microsoft.AspNetCore.Mvc;
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
}
