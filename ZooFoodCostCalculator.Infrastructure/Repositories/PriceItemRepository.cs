using Microsoft.Extensions.Options;
using ZooFoodCostCalculator.Application.Interfaces;
using ZooFoodCostCalculator.Common.Options;
using ZooFoodCostCalculator.Domain.Entities;
using ZooFoodCostCalculator.Domain.Interfaces;

namespace ZooFoodCostCalculator.Infrastructure.Repositories
{
    public class PriceItemRepository : IPriceItemRepository
    {
        private readonly IFileReader _fileReader;
        private readonly FilesOptions _filesConfig;
        public PriceItemRepository(IFileReader fileReader, IOptions<FilesOptions> filesConfig)
        {
            _fileReader = fileReader;
            _filesConfig = filesConfig.Value;
        }
        public List<PricesItem> GetAll(Func<PricesItem, bool> predicate)
        {
            var lines = _fileReader.ReadFileAsLines(_filesConfig.PricesFileName);

            return lines.Select(ParseLine)
                .Where(predicate)
                .ToList();
        }

        public List<PricesItem> GetAll()
        {
            var lines = _fileReader.ReadFileAsLines(_filesConfig.PricesFileName);

            return lines.Select(ParseLine)
                .ToList();
        }

        public PricesItem ParseLine(string line)
        {
            var values = line.Split('=');
            return new PricesItem(values[0], float.Parse(values[1]));
        }
    }
}
