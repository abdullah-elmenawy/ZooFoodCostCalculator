using ZooFoodCostCalculator.Application.Interfaces;
using ZooFoodCostCalculator.Common;

namespace ZooFoodCostCalculator.Application.Services.ValidationDecorators
{
    public class ZooApplicationServiceValidator(IZooApplicationService zooApplicationService) : IZooApplicationService
    {
        public Task<Result<float>> CalculateFoodCost()
        {
            Console.WriteLine($"{nameof(CalculateFoodCost)} validation passed");
            return zooApplicationService.CalculateFoodCost();
        }
    }
}
