using Moq;
using ZooFoodCostCalculator.Application.Interfaces;

namespace ZooFoodCostCalculator.Console.Tests
{
    public class ZooAppTests
    {
        ZooApp app;
        [SetUp]
        public void Setup()
        {
            var mock = new Mock<IZooApplicationService>();
            mock.Setup(c => c.CalculateFoodCost()).ReturnsAsync(1600.34f);

            app = new ZooApp(mock.Object);
        }

        [Test]
        public async Task RunAsync_Should_ExecuteSuccess()
        {
            await app.RunAsync();
        }

        [Test]
        public void RunAsync_ShouldNot_ThrowException()
        {
            Assert.DoesNotThrowAsync(() => app.RunAsync());
        }
    }
}