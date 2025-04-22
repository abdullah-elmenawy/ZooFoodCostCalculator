using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ZooFoodCostCalculator.Application.CommandsHandlers
{
    public class CommandHandlerLogger<TRequest, TResponse>
        (ILogger<CommandHandlerLogger<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation($"[START] Handling request {nameof(TRequest)} - {JsonSerializer.Serialize(request)}");
            var response = await next();
            logger.LogInformation($"[END] Request {nameof(TRequest)} handled with response: {JsonSerializer.Serialize(response)}");
            return response;
        }
    }
}
