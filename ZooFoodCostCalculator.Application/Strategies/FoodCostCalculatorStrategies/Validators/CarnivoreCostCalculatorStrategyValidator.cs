using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooFoodCostCalculator.Application.Interfaces;
using ZooFoodCostCalculator.Domain.Entities;

namespace ZooFoodCostCalculator.Application.Strategies.FoodCostCalculatorStrategies.Validators
{
    public class CarnivoreCostCalculatorStrategyValidator : ICostCalculatorStrategyValidator
    {
        public void Validate (List<PricesItem> prices, List<AnimalFood> animalFoods, List<ZooAnimal> zooAnimals)
        {
            Console.WriteLine($"{nameof(CarnivoreCostCalculatorStrategy)}.{nameof(Validate)} validation started");
            if (prices == null || !prices.Any())
                throw new ArgumentNullException($"{nameof(prices)}");

            if (animalFoods == null || !animalFoods.Any())
                throw new ArgumentNullException($"{nameof(animalFoods)}");

            if (zooAnimals == null || !zooAnimals.Any())
                throw new ArgumentNullException($"{nameof(zooAnimals)}");

            Console.WriteLine($"{nameof(CarnivoreCostCalculatorStrategy)}.{nameof(Validate)} validation passed");
        }
    }
}
