using Microsoft.Extensions.Logging;
using ZooFoodCostCalculator.Application.Interfaces;
using ZooFoodCostCalculator.Common;

namespace ZooFoodCostCalculator.Application.Services.LoggingDecorators
{
    public class LoggingZooApplictionServiceDecorator(IZooApplicationService zooApplicationService, ILogger<LoggingZooApplictionServiceDecorator> logger) : IZooApplicationService
    {
        public async Task<Result<float>> CalculateFoodCost()
        {
            logger.LogInformation($"[START] {nameof(CalculateFoodCost)} started executing");
            var result = await zooApplicationService.CalculateFoodCost();
            logger.LogInformation($"[END] {nameof(CalculateFoodCost)} finishd excuting with result : {result}");
            return result;
        }
    }
}
