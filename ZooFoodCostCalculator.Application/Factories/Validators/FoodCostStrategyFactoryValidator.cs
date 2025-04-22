using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooFoodCostCalculator.Application.Interfaces;
using ZooFoodCostCalculator.Domain.Enums;

namespace ZooFoodCostCalculator.Application.Factories.Validators
{
    public class FoodCostStrategyFactoryValidator(
        IFoodCostStrategyFactory costStrategyFactory
        ) : IFoodCostStrategyFactory
    {
        public IDietTypeCostCalculatorStrategy GetDietTypeStrategy(DietType dietType)
        {
            Console.WriteLine($"{nameof(GetDietTypeStrategy)} validation passed");
            return costStrategyFactory.GetDietTypeStrategy(dietType);
        }
    }
}
