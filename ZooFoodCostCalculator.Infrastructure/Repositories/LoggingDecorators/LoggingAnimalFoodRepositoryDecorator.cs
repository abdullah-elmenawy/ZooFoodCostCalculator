using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZooFoodCostCalculator.Domain.Entities;
using ZooFoodCostCalculator.Domain.Interfaces;
using ZooFoodCostCalculator.Infrastructure.IO;

namespace ZooFoodCostCalculator.Infrastructure.Repositories.LoggingDecorators
{
    public class LoggingAnimalFoodRepositoryDecorator(
        IAnimalFoodRepository animalFoodRepository,
        ILogger<LoggingAnimalFoodRepositoryDecorator> logger) : IAnimalFoodRepository
    {
        public List<AnimalFood> GetAll(Func<AnimalFood, bool> predicate)
        {
            logger.LogInformation($"[START] {nameof(AnimalFoodRepository)}.{nameof(GetAll)} started executing");
            var result = animalFoodRepository.GetAll(predicate);
            logger.LogInformation($"[END] {nameof(AnimalFoodRepository)}.{nameof(GetAll)} finished executing with result : {JsonSerializer.Serialize(result)}");
            return result;
        }

        public List<AnimalFood> GetAll()
        {
            logger.LogInformation($"[START] {nameof(AnimalFoodRepository)}.{nameof(GetAll)} started executing");
            var result = animalFoodRepository.GetAll();
            logger.LogInformation($"[END] {nameof(AnimalFoodRepository)}.{nameof(GetAll)} finished executing with result : {JsonSerializer.Serialize(result)}");
            return result;
        }

        public AnimalFood ParseLine(string line)
        {
            logger.LogInformation($"[START] {nameof(AnimalFoodRepository)}.{nameof(ParseLine)} started executing with input : {line}");
            var result = animalFoodRepository.ParseLine(line);
            logger.LogInformation($"[END] {nameof(AnimalFoodRepository)}.{nameof(ParseLine)} finished executing with result : {JsonSerializer.Serialize(result)}");
            return result;
        }
    }
}
