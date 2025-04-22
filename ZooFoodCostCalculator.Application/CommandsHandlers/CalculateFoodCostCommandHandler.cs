using MediatR;
using ZooFoodCostCalculator.Application.Commands;
using ZooFoodCostCalculator.Application.Interfaces;
using ZooFoodCostCalculator.Domain.Entities;
using ZooFoodCostCalculator.Domain.Enums;
using ZooFoodCostCalculator.Domain.Interfaces;

namespace ZooFoodCostCalculator.Application.CommandsHandlers
{
    public class CalculateFoodCostCommandHandler(
        IFoodCostStrategyFactory costStrategyFactory,
        IPriceItemRepository priceItemRepository,
        IAnimalFoodRepository animalFoodRepository,
        IZooRepository zooRepository) : IRequestHandler<CalculateFoodCostCommand, float>
    {
        public async Task<float> Handle(CalculateFoodCostCommand request, CancellationToken cancellationToken)
        {
            var prices = priceItemRepository.GetAll();
            var totalCost = DietType.List.Sum(dietType => CalculateDietCost(dietType, prices));

            return await Task.FromResult(totalCost);
        }

        private float CalculateDietCost(DietType dietType, List<PricesItem> prices)
        {
            var foods = animalFoodRepository.GetAll(c => c.DietType == dietType);
            var animals = foods.Select(c => c.Animal).ToList();
            var zooAnimals = zooRepository.GetAll(c => animals.Contains(c.Type));

            var costStrategy = costStrategyFactory.GetDietTypeStrategy(dietType);
            return costStrategy.Calculate(prices, foods, zooAnimals);
        }
    }
}
