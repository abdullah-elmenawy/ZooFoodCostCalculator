using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using ZooFoodCostCalculator.Application.Interfaces;
using ZooFoodCostCalculator.Application.Strategies.FoodCostCalculatorStrategies.Validators;
using ZooFoodCostCalculator.Domain.Entities;
using ZooFoodCostCalculator.Domain.Enums;

namespace ZooFoodCostCalculator.Application.Strategies.FoodCostCalculatorStrategies
{
    public class OmnivoreCostCalculatorStrategy(
        ILogger<OmnivoreCostCalculatorStrategy> logger,
        [FromKeyedServices(nameof(OmnivoreCostCalculatorStrategyValidator))] ICostCalculatorStrategyValidator validator)
        : IDietTypeCostCalculatorStrategy
    {
        public float Calculate(List<PricesItem> prices, List<AnimalFood> animalFoods, List<ZooAnimal> zooAnimals)
        {
            logger.LogInformation($"[START] {nameof(OmnivoreCostCalculatorStrategy)}.{nameof(Calculate)} started executing with paramter : {JsonSerializer.Serialize(prices)}");

            validator.Validate(prices, animalFoods, zooAnimals);

            float total = 0;

            var meatPrice = prices.FirstOrDefault(c => c.Food == FoodType.Meat).Price;
            var fruitPrice = prices.FirstOrDefault(c => c.Food == FoodType.Fruit).Price;

            foreach (var animal in zooAnimals)
            {
                var weight = animal.Weight;
                var animalFood = animalFoods.FirstOrDefault(d => d.Animal == animal.Type);

                var foodWeight = animalFood.Ratio * weight;
                var meatWeight = (foodWeight * animalFood.MeatPercentage.Value) / 100;
                var fruitWeight = foodWeight - meatWeight;
                total += fruitWeight * fruitPrice;
                total += meatWeight * meatPrice;
            }

            logger.LogInformation($"[END] {nameof(OmnivoreCostCalculatorStrategy)}.{nameof(Calculate)} finished executing with result : {total}");

            return total;
        }
    }
}
