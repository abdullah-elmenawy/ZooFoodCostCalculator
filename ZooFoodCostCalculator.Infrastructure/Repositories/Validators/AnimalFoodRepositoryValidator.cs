using ZooFoodCostCalculator.Domain.Entities;
using ZooFoodCostCalculator.Domain.Interfaces;

namespace ZooFoodCostCalculator.Infrastructure.Repositories.Validators
{
    public class AnimalFoodRepositoryValidator(
        IAnimalFoodRepository foodRepository
        ) : IAnimalFoodRepository
    {
        public List<AnimalFood> GetAll(Func<AnimalFood, bool> predicate)
        {
            Console.WriteLine($"{nameof(AnimalFoodRepository)}.{nameof(GetAll)} validation started");

            if (predicate == null)
                throw new NullReferenceException($" {nameof(AnimalFoodRepository)}.{nameof(GetAll)} {nameof(predicate)} can't be null");

            var result = foodRepository.GetAll(predicate);

            Console.WriteLine($"{nameof(AnimalFoodRepository)}.{nameof(GetAll)} validation passed");
            return result;
        }

        public List<AnimalFood> GetAll()
        {
            Console.WriteLine($"{nameof(AnimalFoodRepository)}.{nameof(GetAll)} validation started");

            var result = foodRepository.GetAll();

            Console.WriteLine($"{nameof(AnimalFoodRepository)}.{nameof(GetAll)} validation passed");
            return result;
        }

        public AnimalFood ParseLine(string line)
        {
            Console.WriteLine($"{nameof(AnimalFoodRepository)}.{nameof(ParseLine)} validation started");

            if (string.IsNullOrEmpty(line))
                throw new NullReferenceException($" {nameof(AnimalFoodRepository)}.{nameof(ParseLine)} {nameof(line)} can't be null");

            if (!line.Contains(";"))
                throw new ArgumentException("Invalid line to parse");

            var result = foodRepository.ParseLine(line);

            Console.WriteLine($"{nameof(AnimalFoodRepository)}.{nameof(ParseLine)} validation passed");
            return result;
        }
    }
}
