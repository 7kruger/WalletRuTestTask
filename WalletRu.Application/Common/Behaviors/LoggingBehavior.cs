using MediatR;
using Serilog;

namespace WalletRu.Application.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        Log.Information("Messages Request: {Name} {@Request}",
            typeof(TRequest).Name, request);

        return await next();
    }
}