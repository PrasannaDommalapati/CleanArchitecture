using Domain.Dto.Applicant;
using FluentResults;
using Interface;
using MediatR;

namespace Application.Commands.Applicant;

public record ApplicantAddRequest(ApplicantDto Applicant): IRequest<Result<ApplicantAddResponse>>;
public record ApplicantAddResponse(Guid ApplicantId, string FirstName, string LastName);

public class ApplicantAddCommandHandler : IRequestHandler<ApplicantAddRequest, Result<ApplicantAddResponse>>
{
    private readonly IApplicantService _applicantService;

    public ApplicantAddCommandHandler(IApplicantService applicantService)
    {
        _applicantService = applicantService;
    }

    public async Task<Result<ApplicantAddResponse>> Handle(ApplicantAddRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _applicantService.ApplicantAddAsync(request.Applicant, cancellationToken);

            var (applicantId, firstName, lastName, created, lastModified) = response;
            return Result.Ok(new ApplicantAddResponse(response.ApplicantId, response.FirstName, response.LastName));
        }
        catch (Exception ex)
        {
            return Result.Fail($"Failed with an exception {ex.Message}");
        }
    }
}
