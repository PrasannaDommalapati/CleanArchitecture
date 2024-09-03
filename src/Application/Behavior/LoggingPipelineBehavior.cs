using FluentResults;
using Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Behavior;

public class LoggingPipelineBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : Result
{
    private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;
    private readonly IGetCurrentTimeService _getCurrentTime;

    public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger, IGetCurrentTimeService getCurrentTime)
    {
        _logger = logger;
        _getCurrentTime = getCurrentTime;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting request {@RequestName} at {@StartTime}", typeof(TRequest).Name, _getCurrentTime.UtcNow);

        var result = await next();

        if (result.IsFailed)
        {
            _logger.LogError("Request failed {@RequestName}, {@Error} at {@StartTime}", typeof(TRequest).Name,result.Errors, _getCurrentTime.UtcNow);
        }

        _logger.LogInformation("Completed request {@RequestName} at {@StartTime}", typeof(TRequest).Name, _getCurrentTime.UtcNow);

        return result;
    }
}
