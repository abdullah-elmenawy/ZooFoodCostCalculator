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
    public class HerbivoreCostCalculatorStrategyTests
    {
        HerbivoreCostCalculatorStrategy herbivoreStrategy;
        Mock<ICostCalculatorStrategyValidator> validatorMock;

        [SetUp]
        public void Setup()
        {
            var logger = Mock.Of<ILogger<HerbivoreCostCalculatorStrategy>>();

            validatorMock = new Mock<ICostCalculatorStrategyValidator>();
            validatorMock
                .Setup(c => c.Validate(It.IsAny<List<PricesItem>>(), It.IsAny<List<AnimalFood>>(), It.IsAny<List<ZooAnimal>>()))
                .Verifiable();

            herbivoreStrategy = new HerbivoreCostCalculatorStrategy(logger, validatorMock.Object);
        }

        [Test]
        public void Calculate_Should_CallValidator()
        {
            var prices = new List<PricesItem> { new PricesItem(FoodType.Fruit.Name, 5.0f) };
            var animalFoods = new List<AnimalFood> { new AnimalFood(AnimalType.Zebra.Name, 0.1f, FoodType.Fruit.Name, null) };
            var zooAnimals = new List<ZooAnimal> { new ZooAnimal(AnimalType.Zebra.Name, "Kim", 100f) };

            herbivoreStrategy.Calculate(prices, animalFoods, zooAnimals);

            validatorMock.Verify(v => v.Validate(prices, animalFoods, zooAnimals), Times.Once);
        }

        [Test]
        public void Calculate_ShouldReturn_GreaterThanZeroForOneAnimal()
        {
            List<PricesItem> prices =
            [
                new PricesItem(FoodType.Meat.Name, 4.25f),
                new PricesItem(FoodType.Fruit.Name, 15.2f)
            ];
            List<AnimalFood> foods =
            [
                new AnimalFood(AnimalType.Zebra.Name, 0.1f, FoodType.Fruit.Name,null)
            ];
            List<ZooAnimal> zooAnimals =
            [
                new ZooAnimal(AnimalType.Zebra.Name, "Simba", 125.23f)
            ];

            float total = herbivoreStrategy.Calculate(prices, foods, zooAnimals);
            float ceilingTotal = (float)Math.Ceiling(total * 20) / 20;

            Assert.That(total, Is.GreaterThan(0));
            Assert.AreEqual(ceilingTotal, 190.35f);
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
                new AnimalFood(AnimalType.Zebra.Name, 0.1f, FoodType.Fruit.Name,null),
                new AnimalFood(AnimalType.Giraffe.Name, 0.05f, FoodType.Fruit.Name,null)
            ];
            List<ZooAnimal> zooAnimals =
            [
                new ZooAnimal(AnimalType.Zebra.Name, "Simba", 125.23f),
                new ZooAnimal(AnimalType.Giraffe.Name, "Carl", 200.43f)
            ];

            float total = herbivoreStrategy.Calculate(prices, foods, zooAnimals);
            float ceilingTotal = (float)Math.Ceiling(total * 20) / 20;

            Assert.That(total, Is.GreaterThan(0));
            Assert.AreEqual(342.70f, ceilingTotal);
        }
    }
}
