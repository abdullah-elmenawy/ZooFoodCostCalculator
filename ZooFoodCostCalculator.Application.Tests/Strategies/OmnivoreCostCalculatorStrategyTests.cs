using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooFoodCostCalculator.Application.Interfaces;
using ZooFoodCostCalculator.Application.Strategies.FoodCostCalculatorStrategies;
using ZooFoodCostCalculator.Domain.Entities;
using ZooFoodCostCalculator.Domain.Enums;

namespace ZooFoodCostCalculator.Application.Tests.Strategies
{
    public class OmnivoreCostCalculatorStrategyTests
    {
        OmnivoreCostCalculatorStrategy omnivoreStrategy;
        Mock<ICostCalculatorStrategyValidator> validatorMock;

        [SetUp]
        public void Setup()
        {
            var logger = Mock.Of<ILogger<OmnivoreCostCalculatorStrategy>>();

            validatorMock = new Mock<ICostCalculatorStrategyValidator>();
            validatorMock
                .Setup(c => c.Validate(It.IsAny<List<PricesItem>>(), It.IsAny<List<AnimalFood>>(), It.IsAny<List<ZooAnimal>>()))
                .Verifiable();

            omnivoreStrategy = new OmnivoreCostCalculatorStrategy(logger, validatorMock.Object);
        }

        [Test]
        public void Calculate_Should_CallValidator()
        {
            var prices = new List<PricesItem>
            {
                new PricesItem(FoodType.Fruit.Name, 5.0f),
                new PricesItem(FoodType.Meat.Name, 15.0f)
            };
            var animalFoods = new List<AnimalFood>
            {
                new AnimalFood(AnimalType.Wolf.Name, 0.1f, FoodType.Both.Name, 90f)
            };
            var zooAnimals = new List<ZooAnimal>
            {
                new ZooAnimal(AnimalType.Wolf.Name, "Kim", 300f)
            };

            omnivoreStrategy.Calculate(prices, animalFoods, zooAnimals);

            validatorMock.Verify(v => v.Validate(prices, animalFoods, zooAnimals), Times.Once);
        }

        [Test]
        public void Calculate_ShouldReturn_GreaterThanZeroForOneAnimal()
        {
            var prices = new List<PricesItem>
            {
                new PricesItem(FoodType.Fruit.Name, 5.0f),
                new PricesItem(FoodType.Meat.Name, 15.0f)
            };
            var animalFoods = new List<AnimalFood>
            {
                new AnimalFood(AnimalType.Piranha.Name, 0.1f, FoodType.Both.Name, 90f)
            };
            var zooAnimals = new List<ZooAnimal>
            {
                new ZooAnimal(AnimalType.Piranha.Name, "Kim", 300f)
            };

            float total = omnivoreStrategy.Calculate(prices, animalFoods, zooAnimals);
            float ceilingTotal = (float)Math.Ceiling(total * 20) / 20;

            Assert.That(total, Is.GreaterThan(0));
            Assert.AreEqual(ceilingTotal, 420);
        }

        [Test]
        public void Calculate_ShouldReturn_CorrectTotalForMultipleAnimals()
        {
            List<PricesItem> prices =
            [
                new PricesItem("Meat", 4.25f),
                new PricesItem("Fruit", 15.2f)
            ];
            List<AnimalFood> foods =
            [
                new AnimalFood(AnimalType.Piranha.Name, 0.1f, FoodType.Both.Name,70),
                new AnimalFood(AnimalType.Wolf.Name, 0.05f, FoodType.Both.Name,90)
            ];
            List<ZooAnimal> zooAnimals =
            [
                new ZooAnimal(AnimalType.Piranha.Name, "Simba", 50.32f), //Food:5.032 - 3.5224 - 1.5096
                new ZooAnimal(AnimalType.Wolf.Name, "Carl", 200.43f) //Food:10.0215 - 9.01935 - 1.00215
            ];
            //14.9702 + 22.94592
            //38.3322375 + 15.23268

            float total = omnivoreStrategy.Calculate(prices, foods, zooAnimals);
            float ceilingTotal = (float)Math.Ceiling(total * 20) / 20;

            Assert.That(total, Is.GreaterThan(0));
            Assert.AreEqual(91.50f, ceilingTotal);
        }
    }
}
