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
    public class LoggingZooRepositoryDecorator(
        IZooRepository zooRepository,
        ILogger<LoggingZooRepositoryDecorator> logger
        ) : IZooRepository
    {
        public List<ZooAnimal> GetAll(Func<ZooAnimal, bool> predicate)
        {
            logger.LogInformation($"[START] {nameof(ZooRepository)}.{nameof(GetAll)} started executing");
            var result = zooRepository.GetAll(predicate);
            logger.LogInformation($"[END] {nameof(ZooRepository)}.{nameof(GetAll)} finished executing with result : {JsonSerializer.Serialize(result)}");
            return result;
        }

        public List<ZooAnimal> GetAll()
        {
            logger.LogInformation($"[START] {nameof(ZooRepository)}.{nameof(GetAll)} started executing");
            var result = zooRepository.GetAll();
            logger.LogInformation($"[END] {nameof(ZooRepository)}.{nameof(GetAll)} finished executing with result : {JsonSerializer.Serialize(result)}");
            return result;
        }

        public ZooAnimal ParseLine(string line)
        {
            logger.LogInformation($"[START] {nameof(ZooRepository)}.{nameof(ParseLine)} started executing with input : {line}");
            var result = zooRepository.ParseLine(line);
            logger.LogInformation($"[END] {nameof(ZooRepository)}.{nameof(ParseLine)} finished executing with result : {JsonSerializer.Serialize(result)}");
            return result;
        }
    }
}
