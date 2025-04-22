using Ardalis.SmartEnum;

namespace ZooFoodCostCalculator.Domain.Enums
{
    public class DietType(string name, int value) : SmartEnum<DietType>(name, value)
    {
        public static DietType Carnivore = new DietType(nameof(Carnivore), 1);
        public static DietType Herbivore = new DietType(nameof(Herbivore), 2);
        public static DietType Omnivore = new DietType(nameof(Omnivore), 3);
    }
}
