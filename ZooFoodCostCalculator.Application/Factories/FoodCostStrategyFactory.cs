using Microsoft.Extensions.DependencyInjection;
using ZooFoodCostCalculator.Application.Exceptions;
using ZooFoodCostCalculator.Application.Interfaces;
using ZooFoodCostCalculator.Application.Strategies.FoodCostCalculatorStrategies;
using ZooFoodCostCalculator.Domain.Enums;

namespace ZooFoodCostCalculator.Application.Factories
{
    public class FoodCostStrategyFactory(
        IServiceProvider serviceProvider
        ) : IFoodCostStrategyFactory
    {
        public IDietTypeCostCalculatorStrategy GetDietTypeStrategy(DietType dietType)
        {
            if (dietType == DietType.Carnivore)
                return serviceProvider.GetKeyedService<IDietTypeCostCalculatorStrategy>(nameof(CarnivoreCostCalculatorStrategy));
            if (dietType == DietType.Omnivore)
                return serviceProvider.GetKeyedService<IDietTypeCostCalculatorStrategy>(nameof(OmnivoreCostCalculatorStrategy));
            if (dietType == DietType.Herbivore)
                return serviceProvider.GetKeyedService<IDietTypeCostCalculatorStrategy>(nameof(HerbivoreCostCalculatorStrategy));

            throw new DietNotSupportedException($"This diet type {nameof(dietType)} is not supported");
        }
    }
}
