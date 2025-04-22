using MediatR;
using Moq;
using ZooFoodCostCalculator.Application.Commands;
using ZooFoodCostCalculator.Application.Services;

namespace ZooFoodCostCalculator.Application.Tests.Services
{
    public class ZooApplicationServiceTests
    {
        [SetUp]
        public void Setup()
        {
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<CalculateFoodCostCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(5);

            var service = new ZooApplicationService(mockMediator.Object);
        }
        [Test]
        public async Task Calculate_Should_ReturnGreaterThanZero()
        {
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<CalculateFoodCostCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(5);

            var service = new ZooApplicationService(mockMediator.Object);

            var cost = await service.CalculateFoodCost();

            Assert.IsNotNull(cost);
            Assert.Greater(cost.Value, -1);
        }
        [Test]
        public async Task Calculate_Should_CallMediatorSend()
        {
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<CalculateFoodCostCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(5);

            var service = new ZooApplicationService(mockMediator.Object);

            await service.CalculateFoodCost();

            mockMediator.Verify(m => m.Send(It.IsAny<CalculateFoodCostCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
