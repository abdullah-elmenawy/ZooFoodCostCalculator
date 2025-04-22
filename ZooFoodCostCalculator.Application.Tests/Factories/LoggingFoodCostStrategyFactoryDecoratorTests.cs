using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooFoodCostCalculator.Application.Factories.LoggingDecorators;
using ZooFoodCostCalculator.Application.Factories.Validators;
using ZooFoodCostCalculator.Application.Interfaces;
using ZooFoodCostCalculator.Domain.Enums;

namespace ZooFoodCostCalculator.Application.Tests.Factories
{
    public class LoggingFoodCostStrategyFactoryDecoratorTests
    {
        LoggingFoodCostStrategyFactoryDecorator validator;
        [SetUp]
        public void Setup()
        {
            var factoryMock = new Mock<IFoodCostStrategyFactory>();
            var loggerMock = new Mock<ILogger<LoggingFoodCostStrategyFactoryDecorator>>();
            validator = new LoggingFoodCostStrategyFactoryDecorator(factoryMock.Object, loggerMock.Object);
        }

        [Test]
        public void GetDietTypeStrategy_ShouldNot_ThrowException()
        {
            Assert.DoesNotThrow(() => validator.GetDietTypeStrategy(DietType.Carnivore));
        }
    }
}
