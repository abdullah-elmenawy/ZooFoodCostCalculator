using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooFoodCostCalculator.Application.Interfaces;
using ZooFoodCostCalculator.Application.Services.ValidationDecorators;
using ZooFoodCostCalculator.Common;

namespace ZooFoodCostCalculator.Application.Tests.Services
{
    public class LoggingZooApplictionServiceDecorator
    {
        ZooApplicationServiceValidator validator;
        [SetUp]
        public void Setup()
        {
            var zooServiceMock = new Mock<IZooApplicationService>();
            zooServiceMock.Setup(c => c.CalculateFoodCost()).ReturnsAsync(Result<float>.CreateSuccess("Success", 5));
            validator = new ZooApplicationServiceValidator(zooServiceMock.Object);
        }

        [Test]
        public async Task CalculateFoodCost_Should_ExecuteSuccessfully()
        {
            var result = await validator.CalculateFoodCost();

            Assert.NotNull(result);
            Assert.That(5, Is.EqualTo(result.Value));
        }
    }
}
