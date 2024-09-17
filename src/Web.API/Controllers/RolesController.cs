using Application.Commands.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers;

[Route("[controller]")]
[Authorize(Roles = "Admin")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly ISender _mediatr;

    public RolesController(ISender mediatr) => _mediatr = mediatr;

    [HttpPost]
    public async Task<IActionResult> AddRoleToUser([FromBody] AddToRoleRquest toRoleRquest, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(toRoleRquest, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }
}
