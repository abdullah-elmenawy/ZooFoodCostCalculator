using ZooFoodCostCalculator.Common;

namespace ZooFoodCostCalculator.Application.Interfaces
{
    public interface IZooApplicationService
    {
        Task<Result<float>> CalculateFoodCost();
    }
}
