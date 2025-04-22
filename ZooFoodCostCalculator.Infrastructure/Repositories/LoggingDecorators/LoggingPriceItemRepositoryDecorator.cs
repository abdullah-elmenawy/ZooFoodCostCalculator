using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZooFoodCostCalculator.Domain.Entities;
using ZooFoodCostCalculator.Domain.Interfaces;

namespace ZooFoodCostCalculator.Infrastructure.Repositories.LoggingDecorators
{
    public class LoggingPriceItemRepositoryDecorator(
        IPriceItemRepository priceItemRepository,
        ILogger<LoggingPriceItemRepositoryDecorator> logger
        ) : IPriceItemRepository
    {
        public List<PricesItem> GetAll(Func<PricesItem, bool> predicate)
        {
            logger.LogInformation($"[START] {nameof(PriceItemRepository)}.{nameof(GetAll)} started executing");
            var result = priceItemRepository.GetAll(predicate);
            logger.LogInformation($"[END] {nameof(PriceItemRepository)}.{nameof(GetAll)} finished executing with result : {JsonSerializer.Serialize(result)}");
            return result;
        }

        public List<PricesItem> GetAll()
        {
            logger.LogInformation($"[START] {nameof(PriceItemRepository)}.{nameof(GetAll)} started executing");
            var result = priceItemRepository.GetAll();
            logger.LogInformation($"[END] {nameof(PriceItemRepository)}.{nameof(GetAll)} finished executing with result : {JsonSerializer.Serialize(result)}");
            return result;
        }

        public PricesItem ParseLine(string line)
        {
            logger.LogInformation($"[START] {nameof(PriceItemRepository)}.{nameof(ParseLine)} started executing with input : {line}");
            var result = priceItemRepository.ParseLine(line);
            logger.LogInformation($"[END] {nameof(PriceItemRepository)}.{nameof(ParseLine)} finished executing with result : {JsonSerializer.Serialize(result)}");
            return result;
        }
    }
}
