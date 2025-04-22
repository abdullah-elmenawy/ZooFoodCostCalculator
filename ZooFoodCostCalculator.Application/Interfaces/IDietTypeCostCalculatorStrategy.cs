using ZooFoodCostCalculator.Domain.Entities;

namespace ZooFoodCostCalculator.Application.Interfaces
{
    public interface IDietTypeCostCalculatorStrategy
    {
        float Calculate(List<PricesItem> prices, List<AnimalFood> animalFoods, List<ZooAnimal> zooAnimals);
    }
}
