using Domain;
using Domain.Dto.Applicant;
using Interface;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Service;

public class ApplicantService : IApplicantService
{
    private readonly ILogger<ApplicantService> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IGetCurrentTimeService _currentTimeService;

    public ApplicantService(ILogger<ApplicantService> logger, ApplicationDbContext context, IGetCurrentTimeService currentTimeService)
    {
        _logger = logger;
        _context = context;
        _currentTimeService = currentTimeService;
    }

    public async Task<ApplicantResponseDto> ApplicantAddAsync(ApplicantDto applicantDto, CancellationToken cancellationToken)
    {
        try
        {
            var applicantEntity = applicantDto.ToEntity();
            applicantEntity.Created = _currentTimeService.UtcNow;
            applicantEntity.LastModified = _currentTimeService.UtcNow;

            var applicantEntry = await _context.AddAsync(applicantEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return applicantEntry.Entity.ToDto();
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to save the applicant to the db with an exception {@Message}", ex.Message);
            throw;
        }
    }
}
