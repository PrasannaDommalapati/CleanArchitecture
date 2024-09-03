using Microsoft.AspNetCore.Mvc;
using Web.API.Client;

namespace Web.APP.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IApplicantsClient _applicantsClient;

    public WeatherForecastController(IApplicantsClient applicantsClient)
    {
        _applicantsClient = applicantsClient;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> AddApplicant([FromBody] ApplicantDto applicantRequest, CancellationToken cancellationToken)
    {
        return await _applicantsClient.AddApplicantAsync(applicantRequest, cancellationToken);
    }
}
