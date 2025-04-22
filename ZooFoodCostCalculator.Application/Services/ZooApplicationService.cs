using MediatR;
using ZooFoodCostCalculator.Application.Commands;
using ZooFoodCostCalculator.Application.Interfaces;
using ZooFoodCostCalculator.Common;

namespace ZooFoodCostCalculator.Application.Services
{
    public class ZooApplicationService(IMediator mediator) : IZooApplicationService
    {
        public async Task<Result<float>> CalculateFoodCost()
        {
            var calculateCostCommand = new CalculateFoodCostCommand();
            var total = await mediator.Send(calculateCostCommand);

            return Result<float>.CreateSuccess("Success", total);
        }
    }
}
