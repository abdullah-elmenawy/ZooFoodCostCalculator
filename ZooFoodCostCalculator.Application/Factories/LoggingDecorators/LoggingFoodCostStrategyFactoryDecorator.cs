using log4net.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZooFoodCostCalculator.Application.Interfaces;
using ZooFoodCostCalculator.Domain.Enums;

namespace ZooFoodCostCalculator.Application.Factories.LoggingDecorators
{
    public class LoggingFoodCostStrategyFactoryDecorator(
        IFoodCostStrategyFactory costStrategyFactory,
        ILogger<LoggingFoodCostStrategyFactoryDecorator> logger
        ) : IFoodCostStrategyFactory
    {
        public IDietTypeCostCalculatorStrategy GetDietTypeStrategy(DietType dietType)
        {
            logger.LogInformation($"[START] {nameof(GetDietTypeStrategy)} started executing with parameter {JsonSerializer.Serialize(dietType)}");

            var response = costStrategyFactory.GetDietTypeStrategy(dietType);

            logger.LogInformation($"[END] {nameof(GetDietTypeStrategy)} finished executing with response {JsonSerializer.Serialize(response)}");

            return response;
        }
    }
}
