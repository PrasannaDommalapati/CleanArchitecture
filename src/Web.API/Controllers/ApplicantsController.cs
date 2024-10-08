﻿using System.ComponentModel;
using Application.Commands.Applicant;
using Domain.Dto.Applicant;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ApplicantsController : ControllerBase
{
    private readonly ILogger<ApplicantsController> _logger;
    private readonly ISender _mediator;

    public ApplicantsController(ILogger<ApplicantsController> logger, ISender mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    [Description("Add an applicant")]
    public async Task<ActionResult<ApplicantAddResponse>> AddApplicantAsync([FromBody] ApplicantDto applicant, CancellationToken cancellationToken)
    {
        var request = new ApplicantAddRequest(applicant);

        var result = await _mediator.Send(request, cancellationToken);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(string.Join(",",result.Errors.Select(x => x.Message)));
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult GetResponse()
    {
        return Ok(new List<string> { "Apple", "Banana" });
    }
}
