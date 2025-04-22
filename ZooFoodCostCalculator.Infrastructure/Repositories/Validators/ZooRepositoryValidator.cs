using System.Text.RegularExpressions;
using ZooFoodCostCalculator.Domain.Entities;
using ZooFoodCostCalculator.Domain.Interfaces;

namespace ZooFoodCostCalculator.Infrastructure.Repositories.Validators
{
    public class ZooRepositoryValidator(IZooRepository zooRepository) : IZooRepository
    {
        public List<ZooAnimal> GetAll(Func<ZooAnimal, bool> predicate)
        {
            Console.WriteLine($"{nameof(ZooRepository)}.{nameof(GetAll)} validation started");

            if (predicate == null)
                throw new NullReferenceException($" {nameof(ZooRepository)}.{nameof(GetAll)} {nameof(predicate)} can't be null");

            var result = zooRepository.GetAll(predicate);

            Console.WriteLine($"{nameof(ZooRepository)}.{nameof(GetAll)} validation passed");
            return result;
        }

        public List<ZooAnimal> GetAll()
        {
            Console.WriteLine($"{nameof(ZooRepository)}.{nameof(GetAll)} validation started");

            var result = zooRepository.GetAll();

            Console.WriteLine($"{nameof(ZooRepository)}.{nameof(GetAll)} validation passed");
            return result;
        }

        public ZooAnimal ParseLine(string line)
        {
            Console.WriteLine($"{nameof(ZooRepository)}.{nameof(ParseLine)} validation started");

            if (string.IsNullOrEmpty(line))
                throw new NullReferenceException($" {nameof(ZooRepository)}.{nameof(ParseLine)} {nameof(line)} can't be null");

            if (!Regex.IsMatch(line, @"<.+?>"))
                throw new ArgumentException("Invalid XML Content");

            var result = zooRepository.ParseLine(line);

            Console.WriteLine($"{nameof(ZooRepository)}.{nameof(ParseLine)} validation passed");
            return result;
        }
    }
}
