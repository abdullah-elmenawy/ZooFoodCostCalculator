using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooFoodCostCalculator.Application.Commands;
using ZooFoodCostCalculator.Application.CommandsHandlers;
using ZooFoodCostCalculator.Application.Interfaces;
using ZooFoodCostCalculator.Domain.Entities;
using ZooFoodCostCalculator.Domain.Enums;
using ZooFoodCostCalculator.Domain.Interfaces;

namespace ZooFoodCostCalculator.Application.Tests.CommandHandlers
{
    public class CalculateFoodCostCommandHandlerTests
    {
        Mock<IFoodCostStrategyFactory> _strategyFactoryMock = new();
        Mock<IPriceItemRepository> _priceItemRepoMock = new();
        Mock<IAnimalFoodRepository> _animalFoodRepoMock = new();
        Mock<IZooRepository> _zooRepoMock = new();

        CalculateFoodCostCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _handler = new CalculateFoodCostCommandHandler(
                _strategyFactoryMock.Object,
                _priceItemRepoMock.Object,
                _animalFoodRepoMock.Object,
                _zooRepoMock.Object
            );
        }

        [Test]
        public async Task Handle_Should_ReturnTotalCost()
        {
            // Arrange
            var prices = new List<PricesItem> { new PricesItem(FoodType.Meat.Name, 153.2f) };
            _priceItemRepoMock.Setup(r => r.GetAll()).Returns(prices);

            var dietTypes = new List<DietType> { DietType.Carnivore, DietType.Herbivore, DietType.Omnivore };
            //typeof(DietType).GetProperty("List").SetValue(null, dietTypes);

            foreach (var dietType in dietTypes)
            {
                var foods = new List<AnimalFood>
                {
                    new AnimalFood(AnimalType.Lion.Name, 0.1f, FoodType.Meat.Name,null)
                };

                var zooAnimals = new List<ZooAnimal>
                {
                    new ZooAnimal(AnimalType.Lion.Name, "Leo", 200f)
                };

                _animalFoodRepoMock.Setup(r => r.GetAll(It.IsAny<Func<AnimalFood, bool>>())).Returns(foods);
                _zooRepoMock.Setup(r => r.GetAll(It.IsAny<Func<ZooAnimal, bool>>())).Returns(zooAnimals);

                var strategyMock = new Mock<IDietTypeCostCalculatorStrategy>();
                strategyMock.Setup(s => s.Calculate(prices, foods, zooAnimals)).Returns(100f);

                _strategyFactoryMock.Setup(f => f.GetDietTypeStrategy(dietType)).Returns(strategyMock.Object);
            }

            // Act
            var result = await _handler.Handle(new CalculateFoodCostCommand(), CancellationToken.None);

            // Assert
            Assert.AreEqual(100f, result);
        }
    }
}
