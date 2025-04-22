using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooFoodCostCalculator.Application.Strategies.FoodCostCalculatorStrategies.Validators;
using ZooFoodCostCalculator.Domain.Entities;
using ZooFoodCostCalculator.Domain.Enums;

namespace ZooFoodCostCalculator.Application.Tests.Strategies.Validators
{
    public class CarnivoreCostCalculatorStrategyValidatorTests
    {
        CarnivoreCostCalculatorStrategyValidator validator;
        [SetUp]
        public void Setup()
        {
            validator = new CarnivoreCostCalculatorStrategyValidator();
        }
        [Test]
        public void Validate_Should_SucceedWithPopulatedLists()
        {
            var prices = new List<PricesItem>
            {
                new PricesItem(FoodType.Fruit.Name, 15.4f),
                new PricesItem(FoodType.Meat.Name, 45.4f)
            };
            var foods = new List<AnimalFood>
            {
                new AnimalFood(AnimalType.Lion.Name,0.1f, FoodType.Meat.Name, 90)
            };
            var animals = new List<ZooAnimal>
            {
                new ZooAnimal(AnimalType.Lion.Name, "Leo", 320f)
            };

            Assert.DoesNotThrow(() => validator.Validate(prices, foods, animals));
        }

        public void Validate_Should_ThrowExceptionForEmptyPrices()
        {
            var prices = new List<PricesItem>
            {
            };
            var foods = new List<AnimalFood>
            {
                new AnimalFood(AnimalType.Lion.Name,0.1f, FoodType.Meat.Name, 90)
            };
            var animals = new List<ZooAnimal>
            {
                new ZooAnimal(AnimalType.Lion.Name, "Leo", 320f)
            };

            Assert.Throws(typeof(ArgumentNullException), () => validator.Validate(prices, foods, animals));
        }

        public void Validate_Should_ThrowExceptionForEmptyFoods()
        {
            var prices = new List<PricesItem>
            {
                 new PricesItem(FoodType.Fruit.Name, 15.4f),
                new PricesItem(FoodType.Meat.Name, 45.4f)
            };
            var foods = new List<AnimalFood>
            {
            };
            var animals = new List<ZooAnimal>
            {
                new ZooAnimal(AnimalType.Lion.Name, "Leo", 320f)
            };

            Assert.Throws(typeof(ArgumentNullException), () => validator.Validate(prices, foods, animals));
        }

        public void Validate_Should_ThrowExceptionForEmptyAnimals()
        {
            var prices = new List<PricesItem>
            {
                 new PricesItem(FoodType.Fruit.Name, 15.4f),
                new PricesItem(FoodType.Meat.Name, 45.4f)
            };
            var foods = new List<AnimalFood>
            {
                new AnimalFood(AnimalType.Lion.Name,0.1f, FoodType.Meat.Name, 90)
            };
            var animals = new List<ZooAnimal>
            {
            };

            Assert.Throws(typeof(ArgumentNullException), () => validator.Validate(prices, foods, animals));
        }
    }
}
