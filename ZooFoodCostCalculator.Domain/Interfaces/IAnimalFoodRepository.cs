using ZooFoodCostCalculator.Domain.Entities;

namespace ZooFoodCostCalculator.Domain.Interfaces
{
    public interface IAnimalFoodRepository : IRepository<AnimalFood>
    {
        AnimalFood ParseLine(string line);
    }
}
