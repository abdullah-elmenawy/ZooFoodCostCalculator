using ZooFoodCostCalculator.Domain.Enums;

namespace ZooFoodCostCalculator.Domain.Entities
{
    public class AnimalFood(string animalTypeName, float ratio, string food, float? meatPercentage) : BaseEntity
    {
        public AnimalType Animal { get; set; } = AnimalType.FromName(animalTypeName, true);
        public float Ratio { get; set; } = ratio;
        public FoodType Food { get; set; } = FoodType.FromName(food, true);
        public float? MeatPercentage { get; set; } = meatPercentage;

        public DietType DietType
        {
            get
            {
                if (Food == FoodType.Meat)
                    return DietType.Carnivore;

                if (Food == FoodType.Fruit)
                    return DietType.Herbivore;

                if (Food == FoodType.Both)
                    return DietType.Omnivore;

                return null;
            }
        }
    }
}
