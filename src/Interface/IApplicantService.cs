namespace Interface;

public interface IApplicantService
{
    Task ApplicantAddAsync(string? firstName, string? lastName, CancellationToken cancellationToken);
}
