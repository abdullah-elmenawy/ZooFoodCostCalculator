using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooFoodCostCalculator.Application.Factories.Validators;
using ZooFoodCostCalculator.Application.Interfaces;
using ZooFoodCostCalculator.Domain.Enums;

namespace ZooFoodCostCalculator.Application.Tests.Factories
{
    public class FoodCostStrategyFactoryValidatorTests
    {
        FoodCostStrategyFactoryValidator validator;
        [SetUp]
        public void Setup()
        {
            var mock = new Mock<IFoodCostStrategyFactory>();
            validator = new FoodCostStrategyFactoryValidator(mock.Object);
        }

        [Test]
        public void GetDietTypeStrategy_ShouldNot_ThrowException()
        {
            Assert.DoesNotThrow(() => validator.GetDietTypeStrategy(DietType.Carnivore));
        }
    }
}
