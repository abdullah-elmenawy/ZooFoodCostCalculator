using Ardalis.SmartEnum;

namespace ZooFoodCostCalculator.Domain.Enums
{
    public class AnimalType(string name, int value) : SmartEnum<AnimalType>(name, value)
    {
        public static readonly AnimalType Lion = new AnimalType(nameof(Lion), 1);
        public static readonly AnimalType Giraffe = new AnimalType(nameof(Giraffe), 2);
        public static readonly AnimalType Tiger = new AnimalType(nameof(Tiger), 3);
        public static readonly AnimalType Zebra = new AnimalType(nameof(Zebra), 4);
        public static readonly AnimalType Wolf = new AnimalType(nameof(Wolf), 5);
        public static readonly AnimalType Piranha = new AnimalType(nameof(Piranha), 6);
    }
}
