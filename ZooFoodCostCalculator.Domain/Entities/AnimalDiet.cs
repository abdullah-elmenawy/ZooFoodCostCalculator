using ZooFoodCostCalculator.Domain.Enums;

namespace ZooFoodCostCalculator.Domain.Entities
{
    public class AnimalDiet(AnimalType animalType, DietType dietType)
    {
        public AnimalType AnimalType { get; set; } = animalType;
        public DietType DietType { get; set; } = dietType;
    }
}
