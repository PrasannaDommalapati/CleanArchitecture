using Microsoft.AspNetCore.Mvc;
using Web.API.Client;

namespace Web.APP.Server.Controllers;

[ApiController]
[Route("api")]
public class IdentityController : ControllerBase
{
    private readonly IClient _client;

    public IdentityController(IClient client)
    {
        _client = client;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> PostRegisterAsync([FromBody]RegisterRequest registerRequest, CancellationToken cancellationToken)
    {
        await _client.PostRegisterAsync(registerRequest, cancellationToken);
        return Ok();
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<AccessTokenResponse>> PostLoginAsync([FromBody] LoginRequest loginRequest, [FromQuery] bool useCookies, [FromQuery] bool useSession, CancellationToken cancellationToken)
    {
        var response = await _client.PostLoginAsync(useCookies, useSession, loginRequest, cancellationToken);

        return response is not null ? Ok(response) : BadRequest();
    }

    [HttpPost]
    [Route("refresh")]
    public async Task<IActionResult> Refresh()
    {
        await Task.CompletedTask;
        return Ok();
    }

    [HttpGet]
    [Route("confirmEmail")]
    public async Task<IActionResult> ConfirmEmail()
    {
        await Task.CompletedTask;
        return Ok();
    }

    
    [HttpPost]
    [Route("resendConfirmationEmail")]
    public async Task<IActionResult> ResendConfirmationEmail()
    {
        await Task.CompletedTask;
        return Ok();
    }
    
    [HttpPost]
    [Route("forgotPassword")]
    public async Task<IActionResult> ForgotPassword()
    {
        await Task.CompletedTask;
        return Ok();
    }
    
    [HttpPost]
    [Route("resetPassword")]
    public async Task<IActionResult> ResetPassword()
    {
        await Task.CompletedTask;
        return Ok();
    }
    
    [HttpPost]
    [Route("manage/2fa")]
    public async Task<IActionResult> Manage2FA()
    {
        await Task.CompletedTask;
        return Ok();
    }
    
    [HttpPost]
    [Route("manage/info")]
    public async Task<IActionResult> PostManageInfo()
    {
        await Task.CompletedTask;
        return Ok();
    }
    
    [HttpGet]
    [Route("manage/info")]
    public async Task<IActionResult> GetManageInfo()
    {
        await Task.CompletedTask;
        return Ok();
    }


}
