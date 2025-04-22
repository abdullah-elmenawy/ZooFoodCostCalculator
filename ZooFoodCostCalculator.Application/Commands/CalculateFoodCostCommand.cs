using MediatR;

namespace ZooFoodCostCalculator.Application.Commands
{
    public class CalculateFoodCostCommand : IRequest<float>
    {
    }
}
