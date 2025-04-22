using Microsoft.Extensions.Options;
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
    public class ZooRepositoryTests
    {
        ZooRepository repository;
        [SetUp]
        public void SetUp()
        {
            var fileReaderMock = new Mock<IFileReader>();
            fileReaderMock.Setup(c => c.ReadXmlFile(It.IsAny<string>())).Returns(new List<string> {
                "Lion,Simba,160,",
                "Giraffe,Nala,360,",
                "Zebra,Simba,160,"
            });
            var filesOptions = Options.Create(new FilesOptions
            {
                AnimalFoodFileName = "animals.csv",
                FilesPath = "Files",
                PricesFileName = "prices.txt",
                ZooFileName = "zoo.xml"
            });
            repository = new ZooRepository(fileReaderMock.Object, filesOptions);
        }

        [Test]
        public void GetAll_Should_ReturnNonEmpty()
        {
            var items = repository.GetAll();

            Assert.IsNotEmpty(items);
            Assert.That(items.Count, Is.EqualTo(3));
        }

        [Test]
        public void GetAllPredicate_Should_ReturnNonEmpty()
        {
            var items = repository.GetAll(c => c.Type == AnimalType.Lion);

            Assert.IsNotEmpty(items);
            Assert.That(items.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetAllPredicateFoodTypeFilter_Should_ReturnNonEmpty()
        {
            var items = repository.GetAll(c => c.Type == AnimalType.Lion || c.Type == AnimalType.Giraffe);

            Assert.IsNotEmpty(items);
            Assert.That(items.Count, Is.EqualTo(2));
        }

        [Test]
        public void ParseLine_Should_ReturnNotNull()
        {
            var line = repository.ParseLine("Lion,Simba,160,");

            Assert.NotNull(line);
            Assert.IsTrue(line.Type == AnimalType.Lion);
            Assert.AreEqual("Simba", line.Name);
            Assert.AreEqual(line.Weight, 160);
        }
    }
}
