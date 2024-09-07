using Application.Commands.Authentication;
using Domain.Dto.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("signup")]
    public async Task<IActionResult> RegisterAsync([FromBody] SignupDto user, CancellationToken cancellationToken)
    {
        var request = new SignupRequest(user);
        var result = await _mediator.Send(request, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Reasons);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LoginAsync([FromBody] SignInDto credentials, CancellationToken cancellationToken)
    {
        var request = new SignInRequest(credentials);
        var result = await _mediator.Send(request, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Reasons);
    }
}
