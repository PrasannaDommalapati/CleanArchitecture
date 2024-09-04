namespace Domain.Dto.Applicant;

public record ApplicantResponseDto(Guid ApplicantId, string FirstName, string LastName, DateTimeOffset Created, DateTimeOffset LastModified);