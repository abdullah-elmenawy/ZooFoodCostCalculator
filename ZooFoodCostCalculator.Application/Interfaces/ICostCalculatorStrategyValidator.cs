using ZooFoodCostCalculator.Domain.Entities;

namespace ZooFoodCostCalculator.Application.Interfaces
{
    public interface ICostCalculatorStrategyValidator
    {
        void Validate(List<PricesItem> prices, List<AnimalFood> animalFoods, List<ZooAnimal> zooAnimals);
    }
}
