using ZooFoodCostCalculator.Domain.Enums;

namespace ZooFoodCostCalculator.Application.Interfaces
{
    public interface IFoodCostStrategyFactory
    {
        IDietTypeCostCalculatorStrategy GetDietTypeStrategy(DietType dietType);
    }
}
