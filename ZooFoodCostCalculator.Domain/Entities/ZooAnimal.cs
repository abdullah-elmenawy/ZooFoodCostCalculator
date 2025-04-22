using ZooFoodCostCalculator.Domain.Enums;

namespace ZooFoodCostCalculator.Domain.Entities
{
    public class ZooAnimal(string animalType, string name, float weight) : BaseEntity
    {
        public AnimalType Type { get; set; } = AnimalType.FromName(animalType, true);
        public string Name { get; set; } = name;
        public float Weight { get; set; } = weight;
    }
}
