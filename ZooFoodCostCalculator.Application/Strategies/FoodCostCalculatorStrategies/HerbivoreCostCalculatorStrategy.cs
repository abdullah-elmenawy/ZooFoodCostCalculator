using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using ZooFoodCostCalculator.Application.Interfaces;
using ZooFoodCostCalculator.Application.Strategies.FoodCostCalculatorStrategies.Validators;
using ZooFoodCostCalculator.Domain.Entities;
using ZooFoodCostCalculator.Domain.Enums;

namespace ZooFoodCostCalculator.Application.Strategies.FoodCostCalculatorStrategies
{
    public class HerbivoreCostCalculatorStrategy(
        ILogger<HerbivoreCostCalculatorStrategy> logger,
        [FromKeyedServices(nameof(HerbivoreCostCalculatorStrategyValidator))] ICostCalculatorStrategyValidator validator
        ) : IDietTypeCostCalculatorStrategy
    {
        public float Calculate(List<PricesItem> prices, List<AnimalFood> animalFoods, List<ZooAnimal> zooAnimals)
        {
            logger.LogInformation($"[START] {nameof(HerbivoreCostCalculatorStrategy)}.{nameof(Calculate)} started executing with paramter : {JsonSerializer.Serialize(prices)}");

            validator.Validate(prices, animalFoods, zooAnimals);

            float total = 0;

            var foodPrice = prices.FirstOrDefault(c => c.Food == FoodType.Fruit).Price;

            foreach (var animal in zooAnimals)
            {
                var animalFoodRatio = animalFoods.FirstOrDefault(d => d.Animal == animal.Type).Ratio;

                var foodWeight = animalFoodRatio * animal.Weight;

                total += foodWeight * foodPrice;
            }

            logger.LogInformation($"[END] {nameof(HerbivoreCostCalculatorStrategy)}.{nameof(Calculate)} finished executing with result : {total}");

            return total;
        }
    }
}
