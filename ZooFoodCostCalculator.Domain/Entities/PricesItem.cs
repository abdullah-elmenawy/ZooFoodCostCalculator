using ZooFoodCostCalculator.Domain.Enums;

namespace ZooFoodCostCalculator.Domain.Entities
{
    public class PricesItem(string name, float price) : BaseEntity
    {
        public FoodType Food { get; set; } = FoodType.FromName(name, true);
        public float Price { get; set; } = price;
    }
}
