using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using ZooFoodCostCalculator.Application.Interfaces;
using ZooFoodCostCalculator.Application.Strategies.FoodCostCalculatorStrategies;
using ZooFoodCostCalculator.Domain.Entities;
using ZooFoodCostCalculator.Domain.Enums;

namespace ZooFoodCostCalculator.Application.Tests.Strategies
{
    public class CarnivoreCostCalculatorStrategyTests
    {
        CarnivoreCostCalculatorStrategy carnivoreStrategy;
        Mock<ICostCalculatorStrategyValidator> validatorMock;
        [SetUp]
        public void Setup()
        {
            var logger = Mock.Of<ILogger<CarnivoreCostCalculatorStrategy>>();

            validatorMock = new Mock<ICostCalculatorStrategyValidator>();
            validatorMock
                .Setup(c => c.Validate(It.IsAny<List<PricesItem>>(), It.IsAny<List<AnimalFood>>(), It.IsAny<List<ZooAnimal>>()))
                .Verifiable();

            carnivoreStrategy = new CarnivoreCostCalculatorStrategy(logger, validatorMock.Object);
        }

        [Test]
        public void Calculate_Should_CallValidator()
        {
            var prices = new List<PricesItem> { new PricesItem(FoodType.Meat.Name, 5.0f) };
            var animalFoods = new List<AnimalFood> { new AnimalFood(AnimalType.Lion.Name, 0.1f, FoodType.Meat.Name, null) };
            var zooAnimals = new List<ZooAnimal> { new ZooAnimal(AnimalType.Lion.Name, "Kim", 100f) };

            carnivoreStrategy.Calculate(prices, animalFoods, zooAnimals);

            validatorMock.Verify(v => v.Validate(prices, animalFoods, zooAnimals), Times.Once);
        }

        [Test]
        public void Calculate_ShouldReturn_GreaterThanZeroForOneAnimal()
        {
            List<PricesItem> prices =
            [
                new PricesItem("Meat", 15.2f),
                new PricesItem("Fruit", 4.25f)
            ];
            List<AnimalFood> foods =
            [
                new AnimalFood("Lion", 0.1f, "Meat",null)
            ];
            List<ZooAnimal> zooAnimals =
            [
                new ZooAnimal("Lion", "Simba", 125.23f)
            ];

            float total = carnivoreStrategy.Calculate(prices, foods, zooAnimals);
            float ceilingTotal = (float)Math.Ceiling(total * 20) / 20;

            Assert.That(total, Is.GreaterThan(0));
            Assert.AreEqual(ceilingTotal, 190.35f);
        }

        [Test]
        public void Calculate_ShouldReturn_CorrectTotalForMultipleAnimals()
        {
            List<PricesItem> prices =
            [
                new PricesItem("Meat", 15.2f),
                new PricesItem("Fruit", 4.25f)
            ];
            List<AnimalFood> foods =
            [
                new AnimalFood("Lion", 0.1f, "Meat",null),
                new AnimalFood("Tiger", 0.05f, "Meat",null)
            ];
            List<ZooAnimal> zooAnimals =
            [
                new ZooAnimal(AnimalType.Lion.Name, "Simba", 125.23f),
                new ZooAnimal(AnimalType.Tiger.Name, "Carl", 200.43f)
            ];

            float total = carnivoreStrategy.Calculate(prices, foods, zooAnimals);
            float ceilingTotal = (float)Math.Ceiling(total * 20) / 20;

            Assert.That(total, Is.GreaterThan(0));
            Assert.AreEqual(342.70f, ceilingTotal);
        }
    }
}