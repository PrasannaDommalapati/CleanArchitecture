using Domain.Dto.Applicant;
using FluentResults;
using Interface;
using MediatR;

namespace Application.Commands.Applicant;

public record ApplicantAddRequest(ApplicantDto Applicant): IRequest<Result>;
public record ApplicantAddResponse(Guid Id);

public class ApplicantAddCommandHandler : IRequestHandler<ApplicantAddRequest, Result>
{
    private readonly IApplicantService _applicantService;

    public ApplicantAddCommandHandler(IApplicantService applicantService)
    {
        _applicantService = applicantService;
    }

    public async Task<Result> Handle(ApplicantAddRequest request, CancellationToken cancellationToken)
    {
        if(string.IsNullOrEmpty(request.Applicant.FirstName) || string.IsNullOrEmpty(request.Applicant.LastName))
        {
            return Result.Fail("sdnsnnslndflnsd");
        }
        await _applicantService.ApplicantAddAsync(request.Applicant.FirstName, request.Applicant.LastName, cancellationToken);
        return Result.Ok();
    }
}
