using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Domain.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Web.API.Middlewares;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IConfiguration _configuration;

    public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
                                      ILoggerFactory logger,
                                      UrlEncoder encoder,
                                      ISystemClock clock,
                                      IConfiguration configuration) : base(options, logger, encoder, clock) => _configuration = configuration;

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return AuthenticateResult.Fail("Missing Authorization Header");
        }

        BasicAuthHttpClientConfig basicAuthUser;
        try
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            if (string.IsNullOrEmpty(authHeader.Parameter))
            {
                return AuthenticateResult.Fail("Authorization header is invalid.");
            }
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');

            if (credentials.Length != 2)
            {
                return AuthenticateResult.Fail("Invalid credentials format.");
            }

            var username = credentials[0];
            var password = credentials[1];

            basicAuthUser = _configuration.GetSection("BasicAuthUser").Get<BasicAuthHttpClientConfig>();
            if (basicAuthUser != null && username.Equals(basicAuthUser.Username) && password.Equals(basicAuthUser.Password))
            {
                basicAuthUser.IsAuthenticated = true;
            }
        }
        catch
        {
            return AuthenticateResult.Fail("Invalid Authorization Header");
        }

        if (!basicAuthUser.IsAuthenticated)
        {
            return AuthenticateResult.Fail("Invalid username or password");
        }

        var identity = new ClaimsIdentity(claims: [], authenticationType: Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        await Task.CompletedTask;
        return AuthenticateResult.Success(ticket);
    }
}