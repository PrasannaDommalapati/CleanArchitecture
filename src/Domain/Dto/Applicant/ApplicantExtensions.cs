using ApplicantEntity = Domain.Entities.Applicant;

namespace Domain.Dto.Applicant;

public static class ApplicantExtensions
{
    public static ApplicantEntity ToEntity(this ApplicantDto applicant)
    {
        return new ApplicantEntity
        {
            FirstName = applicant.FirstName,
            LastName = applicant.LastName,
        };
    }
    
    public static ApplicantResponseDto ToDto(this ApplicantEntity applicant)
    {
        return new ApplicantResponseDto(applicant.Id, applicant.FirstName, applicant.LastName, applicant.Created, applicant.LastModified);
    }
}
