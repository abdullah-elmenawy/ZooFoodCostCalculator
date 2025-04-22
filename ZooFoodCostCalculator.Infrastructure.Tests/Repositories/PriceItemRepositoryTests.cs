using Microsoft.Extensions.Options;
using Moq;
using ZooFoodCostCalculator.Application.Interfaces;
using ZooFoodCostCalculator.Common.Options;
using ZooFoodCostCalculator.Domain.Enums;
using ZooFoodCostCalculator.Infrastructure.Repositories;

namespace ZooFoodCostCalculator.Infrastructure.Tests.Repositories
{
    public class PriceItemRepositoryTests
    {
        PriceItemRepository repository;
        [SetUp]
        public void SetUp()
        {
            var fileReaderMock = new Mock<IFileReader>();
            fileReaderMock.Setup(c => c.ReadFileAsLines(It.IsAny<string>())).Returns(new[] {
                "Meat=12.56",
                "Fruit=5.60"
            });
            var filesOptions = Options.Create(new FilesOptions
            {
                AnimalFoodFileName = "animals.csv",
                FilesPath = "Files",
                PricesFileName = "prices.txt",
                ZooFileName = "zoo.xml"
            });
            repository = new PriceItemRepository(fileReaderMock.Object, filesOptions);
        }

        [Test]
        public void GetAll_Should_ReturnNonEmpty()
        {
            var items = repository.GetAll();

            Assert.IsNotEmpty(items);
            Assert.That(items.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetAllPredicate_Should_ReturnNonEmpty()
        {
            var items = repository.GetAll(c => c.Food == FoodType.Meat);

            Assert.IsNotEmpty(items);
            Assert.That(items.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetAllPredicateFoodTypeFilter_Should_ReturnNonEmpty()
        {
            var items = repository.GetAll(c => c.Food == FoodType.Meat || c.Food == FoodType.Fruit);

            Assert.IsNotEmpty(items);
            Assert.That(items.Count, Is.EqualTo(2));
        }

        [Test]
        public void ParseLine_Should_ReturnNotNull()
        {
            var line = repository.ParseLine("Meat=12.56");

            Assert.NotNull(line);
            Assert.IsTrue(line.Food == FoodType.Meat);
            Assert.AreEqual(line.Price, 12.56f);
        }
    }
}
