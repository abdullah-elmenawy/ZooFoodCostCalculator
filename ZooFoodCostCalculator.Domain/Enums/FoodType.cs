using Ardalis.SmartEnum;

namespace ZooFoodCostCalculator.Domain.Enums
{
    public class FoodType(string name, int id) : SmartEnum<FoodType>(name, id)
    {
        public static readonly FoodType Meat = new FoodType(nameof(Meat), 1);
        public static readonly FoodType Fruit = new FoodType(nameof(Fruit), 2);
        public static readonly FoodType Both = new FoodType(nameof(Both), 3);
    }
}
