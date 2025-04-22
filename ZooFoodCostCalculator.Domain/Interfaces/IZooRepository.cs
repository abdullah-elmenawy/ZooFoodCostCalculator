using ZooFoodCostCalculator.Domain.Entities;

namespace ZooFoodCostCalculator.Domain.Interfaces
{
    public interface IZooRepository : IRepository<ZooAnimal>
    {
        ZooAnimal ParseLine(string line);
    }
}
