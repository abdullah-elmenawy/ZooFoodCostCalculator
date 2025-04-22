using ZooFoodCostCalculator.Domain.Entities;

namespace ZooFoodCostCalculator.Domain.Interfaces
{
    public interface IPriceItemRepository : IRepository<PricesItem>
    {
        PricesItem ParseLine(string line);
    }
}
