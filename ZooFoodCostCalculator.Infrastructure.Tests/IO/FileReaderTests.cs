using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;
using ZooFoodCostCalculator.Common.Options;
using ZooFoodCostCalculator.Infrastructure.IO;

namespace ZooFoodCostCalculator.Infrastructure.Tests.IO
{
    public class Tests
    {
        FileReader fileReader;
        IOptions<FilesOptions> fileOptions;
        [SetUp]
        public void Setup()
        {
            fileOptions = Options.Create(new FilesOptions
            {
                AnimalFoodFileName = "animals.csv",
                FilesPath = "Files",
                PricesFileName = "prices.txt",
                ZooFileName = "zoo.xml"
            });
            fileReader = new FileReader(fileOptions);
        }

        [Test]
        public void ReadAsLines_Should_ReturnLines()
        {
            var lines = fileReader.ReadFileAsLines(fileOptions.Value.PricesFileName);
            Assert.Greater(lines.Length, 0);
        }

        [Test]
        public void ReadAsLines_Should_ReturnEqualsLines()
        {
            var lines = fileReader.ReadFileAsLines(fileOptions.Value.PricesFileName);

            Assert.True(lines.All(c => c.Contains('=')));
        }

        [Test]
        public void ReadXmlFile_Should_ReturnLines()
        {
            var lines = fileReader.ReadFileAsLines(fileOptions.Value.ZooFileName);
            Assert.Greater(lines.Length, 0);
        }

        [Test]
        public void ReadXmlFile_Should_ReturnCommaSeparatedLines()
        {
            var lines = fileReader.ReadFileAsLines(fileOptions.Value.ZooFileName);

            Assert.True(lines.All(c => Regex.IsMatch(c, @"<.+?>")));
        }
    }
}