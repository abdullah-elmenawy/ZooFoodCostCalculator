using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooFoodCostCalculator.Application.Interfaces;
using ZooFoodCostCalculator.Common.Options;
using ZooFoodCostCalculator.Domain.Enums;
using ZooFoodCostCalculator.Infrastructure.Repositories;

namespace ZooFoodCostCalculator.Infrastructure.Tests.Repositories
{
    public class AnimalFoodRepositoryTests
    {
        AnimalFoodRepository repository;
        [SetUp]
        public void SetUp()
        {
            var fileReaderMock = new Mock<IFileReader>();
            fileReaderMock.Setup(c => c.ReadFileAsLines(It.IsAny<string>())).Returns(new[] {
                "Lion;0.10;meat;",
            "Tiger;0.09;meat;",
            "Giraffe;0.08;fruit;",
            "Zebra;0.08;fruit;",
            "Wolf;0.07;both;90 %",
            "Piranha;0.5;both;50 %"
            });
            var filesOptions = Options.Create(new FilesOptions
            {
                AnimalFoodFileName = "animals.csv",
                FilesPath = "Files",
                PricesFileName = "prices.txt",
                ZooFileName = "zoo.xml"
            });
            repository = new AnimalFoodRepository(fileReaderMock.Object, filesOptions);
        }

        [Test]
        public void GetAll_Should_ReturnNonEmpty()
        {
            var items = repository.GetAll();

            Assert.IsNotEmpty(items);
            Assert.That(items.Count, Is.EqualTo(6));
        }

        [Test]
        public void GetAllPredicate_Should_ReturnNonEmpty()
        {
            var items = repository.GetAll(c => c.Food == FoodType.Meat);

            Assert.IsNotEmpty(items);
            Assert.That(items.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetAllPredicateFoodTypeFilter_Should_ReturnNonEmpty()
        {
            var items = repository.GetAll(c => c.Food == FoodType.Meat || c.Food == FoodType.Fruit);

            Assert.IsNotEmpty(items);
            Assert.That(items.Count, Is.EqualTo(4));
        }

        [Test]
        public void ParseLine_Should_ReturnNotNull()
        {
            var line = repository.ParseLine("Lion;0.10;meat;");

            Assert.NotNull(line);
            Assert.IsTrue(line.Animal == AnimalType.Lion);
            Assert.AreEqual(line.Ratio, 0.10f);
            Assert.AreEqual(line.Ratio, 0.10f);
        }
    }
}
