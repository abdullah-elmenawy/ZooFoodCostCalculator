using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooFoodCostCalculator.Domain.Entities;
using ZooFoodCostCalculator.Domain.Enums;
using ZooFoodCostCalculator.Domain.Interfaces;
using ZooFoodCostCalculator.Infrastructure.Repositories.Validators;

namespace ZooFoodCostCalculator.Infrastructure.Tests.Repositories.Validators
{
    public class ZooRepositoryValidatorTests
    {
        ZooRepositoryValidator validator;
        [SetUp]
        public void Setup()
        {
            var meatPrice = new ZooAnimal(AnimalType.Lion.Name, "Simba", 153.4f);
            var repositoryMock = new Mock<IZooRepository>();
            repositoryMock.Setup(c => c.GetAll()).Returns(new List<Domain.Entities.ZooAnimal> { meatPrice });
            repositoryMock.Setup(c => c.GetAll(It.IsAny<Func<ZooAnimal, bool>>())).Returns(new List<Domain.Entities.ZooAnimal> { meatPrice });
            repositoryMock.Setup(c => c.ParseLine(It.IsAny<string>())).Returns(meatPrice);

            validator = new ZooRepositoryValidator(repositoryMock.Object);
        }

        [Test]
        public void GetAll_Should_PassValidation()
        {
            var result = validator.GetAll();
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetAll_ShouldNot_ThrowException()
        {
            Assert.DoesNotThrow(() => validator.GetAll());
        }

        [Test]
        public void GetAllPredicate_Should_PassValidation()
        {
            var result = validator.GetAll(c => c.Type == AnimalType.Lion);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetAllPredicate_ShouldNot_ThrowException()
        {
            Assert.DoesNotThrow(() => validator.GetAll(c => c.Type == AnimalType.Lion));
        }

        [Test]
        public void GetAllNullPredicate_Should_ThrowException()
        {
            Assert.Throws(typeof(NullReferenceException), () => validator.GetAll(null));
        }

        [Test]
        public void ParseLine_Should_PassValidation()
        {
            var result = validator.ParseLine("<Tiger name='Tolkien' kg='139'/>");
            Assert.IsNotNull(result);
        }

        [Test]
        public void ParseLine_Should_ThrowException()
        {
            Assert.Throws(typeof(NullReferenceException), () => validator.ParseLine(string.Empty));
        }

        [Test]
        public void ParseLine_Should_ThrowArgumentException()
        {
            Assert.Throws(typeof(ArgumentException), () => validator.ParseLine("line"));
        }
    }
}
