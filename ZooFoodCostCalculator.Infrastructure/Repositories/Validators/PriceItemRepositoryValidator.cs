using ZooFoodCostCalculator.Domain.Entities;
using ZooFoodCostCalculator.Domain.Interfaces;

namespace ZooFoodCostCalculator.Infrastructure.Repositories.Validators
{
    public class PriceItemRepositoryValidator(IPriceItemRepository priceItemRepository) : IPriceItemRepository
    {
        public List<PricesItem> GetAll(Func<PricesItem, bool> predicate)
        {
            Console.WriteLine($"{nameof(PriceItemRepository)}.{nameof(GetAll)} validation started");

            if (predicate == null)
                throw new NullReferenceException($" {nameof(PriceItemRepository)}.{nameof(GetAll)} {nameof(predicate)} can't be null");

            var result = priceItemRepository.GetAll(predicate);

            Console.WriteLine($"{nameof(PriceItemRepository)}.{nameof(GetAll)} validation passed");
            return result;
        }

        public List<PricesItem> GetAll()
        {
            Console.WriteLine($"{nameof(PriceItemRepository)}.{nameof(GetAll)} validation started");

            var result = priceItemRepository.GetAll();

            Console.WriteLine($"{nameof(PriceItemRepository)}.{nameof(GetAll)} validation passed");
            return result;
        }

        public PricesItem ParseLine(string line)
        {
            Console.WriteLine($"{nameof(PriceItemRepository)}.{nameof(ParseLine)} validation started");

            if (string.IsNullOrEmpty(line))
                throw new NullReferenceException($" {nameof(PriceItemRepository)}.{nameof(ParseLine)} {nameof(line)} can't be null");

            if (!line.Contains("="))
                throw new ArgumentException("Invalid line to parse");

            var result = priceItemRepository.ParseLine(line);

            Console.WriteLine($"{nameof(PriceItemRepository)}.{nameof(ParseLine)} validation passed");
            return result;
        }
    }
}
