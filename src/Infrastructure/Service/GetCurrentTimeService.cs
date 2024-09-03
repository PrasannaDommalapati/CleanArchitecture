using Interface;

namespace Infrastructure.Service;

public class GetCurrentTimeService : IGetCurrentTimeService
{
    public DateTime Now => DateTime.Now;
    public DateTime UtcNow => DateTime.UtcNow;
}
