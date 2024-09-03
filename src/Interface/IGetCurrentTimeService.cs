namespace Interface;
public interface IGetCurrentTimeService
{
    DateTime Now { get; }
    DateTime UtcNow { get; }
}
