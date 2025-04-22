using ZooFoodCostCalculator.Application.Interfaces;
using ZooFoodCostCalculator.Domain.Entities;

namespace ZooFoodCostCalculator.Application.Strategies.FoodCostCalculatorStrategies.Validators
{
    public class OmnivoreCostCalculatorStrategyValidator : ICostCalculatorStrategyValidator
    {
        public void Validate(List<PricesItem> prices, List<AnimalFood> animalFoods, List<ZooAnimal> zooAnimals)
        {
            Console.WriteLine($"{nameof(OmnivoreCostCalculatorStrategy)}.{nameof(Validate)} validation started");
            if (prices == null || !prices.Any())
                throw new ArgumentNullException($"{nameof(prices)} can't be null");

            if (animalFoods == null || !animalFoods.Any())
                throw new ArgumentNullException($"{nameof(animalFoods)} can't be null");

            if (zooAnimals == null || !zooAnimals.Any())
                throw new ArgumentNullException($"{nameof(zooAnimals)} can't be null");

            if (animalFoods.Any(c => !c.MeatPercentage.HasValue))
                throw new ArgumentNullException($"{nameof(AnimalFood.MeatPercentage)} can't be null");

            Console.WriteLine($"{nameof(OmnivoreCostCalculatorStrategy)}.{nameof(Validate)} validation passed");
        }
    }
}
