using Domain.Dto.Applicant;

namespace Interface;

public interface IApplicantService
{
    Task<ApplicantResponseDto> ApplicantAddAsync(ApplicantDto applicantDto, CancellationToken cancellationToken);
}
