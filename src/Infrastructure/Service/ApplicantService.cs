using Interface;

namespace Infrastructure.Service;

public class ApplicantService : IApplicantService
{
    public async Task ApplicantAddAsync(string? firstName, string? lastName, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}
