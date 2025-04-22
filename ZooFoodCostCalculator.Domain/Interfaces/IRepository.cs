using ZooFoodCostCalculator.Domain.Entities;

namespace ZooFoodCostCalculator.Domain.Interfaces
{
    public interface IRepository<T>
        where T : BaseEntity
    {
        List<T> GetAll(Func<T, bool> predicate);
        List<T> GetAll();
    }
}
