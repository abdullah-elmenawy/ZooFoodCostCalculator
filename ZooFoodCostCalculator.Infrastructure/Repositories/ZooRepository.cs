using Microsoft.Extensions.Options;
using ZooFoodCostCalculator.Application.Interfaces;
using ZooFoodCostCalculator.Common.Options;
using ZooFoodCostCalculator.Domain.Entities;
using ZooFoodCostCalculator.Domain.Interfaces;

namespace ZooFoodCostCalculator.Infrastructure.Repositories
{
    public class ZooRepository : IZooRepository
    {
        private readonly IFileReader _fileReader;
        private readonly FilesOptions _filesConfig;
        public ZooRepository(IFileReader fileReader, IOptions<FilesOptions> filesConfig)
        {
            _fileReader = fileReader;
            _filesConfig = filesConfig.Value;
        }
        public List<ZooAnimal> GetAll(Func<ZooAnimal, bool> predicate)
        {
            var lines = _fileReader.ReadXmlFile(_filesConfig.ZooFileName);

            return lines.Select(ParseLine)
                .Where(predicate)
                .ToList();
        }

        public List<ZooAnimal> GetAll()
        {
            var lines = _fileReader.ReadXmlFile(_filesConfig.ZooFileName);

            return lines.Select(ParseLine)
                .ToList();
        }

        public ZooAnimal ParseLine(string line)
        {
            var values = line.Split(',');
            return new ZooAnimal(values[0], values[1], float.Parse(values[2]));
        }
    }
}
