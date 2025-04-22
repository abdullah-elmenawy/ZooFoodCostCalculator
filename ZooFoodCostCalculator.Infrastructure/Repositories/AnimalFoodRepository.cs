using Microsoft.Extensions.Options;
using ZooFoodCostCalculator.Application.Interfaces;
using ZooFoodCostCalculator.Common.Options;
using ZooFoodCostCalculator.Domain.Entities;
using ZooFoodCostCalculator.Domain.Interfaces;

namespace ZooFoodCostCalculator.Infrastructure.Repositories
{
    public class AnimalFoodRepository : IAnimalFoodRepository
    {
        private readonly FilesOptions _filesConfig;
        private readonly IFileReader _fileReader;
        public AnimalFoodRepository(IFileReader fileReader, IOptions<FilesOptions> filesConfig)
        {
            _fileReader = fileReader;
            _filesConfig = filesConfig.Value;
        }
        public List<AnimalFood> GetAll(Func<AnimalFood, bool> predicate)
        {
            var lines = _fileReader.ReadFileAsLines(_filesConfig.AnimalFoodFileName);

            return lines.Select(ParseLine)
                .Where(predicate)
                .ToList();
        }

        public List<AnimalFood> GetAll()
        {
            var lines = _fileReader.ReadFileAsLines(_filesConfig.AnimalFoodFileName);

            return lines.Select(ParseLine)
                .ToList();
        }

        public AnimalFood ParseLine(string line)
        {
            var values = line.Split(';');
            float? meatPercentage = string.IsNullOrEmpty(values[3]) ? null : float.Parse(values[3].TrimEnd('%'));
            var food = new AnimalFood(values[0], float.Parse(values[1]), values[2], meatPercentage);
            return food;
        }
    }
}
