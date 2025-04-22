using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZooFoodCostCalculator.Application.Interfaces;
using ZooFoodCostCalculator.Common.Options;

namespace ZooFoodCostCalculator.Infrastructure.IO.Validators
{
    public class FileReaderValidator : IFileReader
    {
        private readonly FilesOptions _filesConfig;
        private readonly IFileReader _fileReader;
        public FileReaderValidator(IFileReader fileReader, IOptions<FilesOptions> filesConfig)
        {
            _filesConfig = filesConfig.Value;
            _fileReader = fileReader;
        }
        public string[] ReadFileAsLines(string fileName)
        {
            Console.WriteLine($"{nameof(FileReader)}.{nameof(ReadFileAsLines)} validation started");

            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException($"{nameof(fileName)} parameter is empty");

            var filePath = $@"{_filesConfig.FilesPath}\{fileName}";
            var fileExists = Path.Exists(filePath);
            if (!fileExists)
                throw new FileNotFoundException($"{nameof(fileName)} : {fileName} doesn't exist");

            var fullPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (string.IsNullOrEmpty(fullPath))
                throw new DirectoryNotFoundException($"Path : {fullPath} doesn't exist");

            var result = _fileReader.ReadFileAsLines(fileName);

            Console.WriteLine($"{nameof(FileReader)}.{nameof(ReadFileAsLines)} validation passed");

            return result;
        }

        public List<string> ReadXmlFile(string fileName)
        {
            Console.WriteLine($"{nameof(FileReader)}.{nameof(ReadXmlFile)} validation started");

            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException($"{nameof(fileName)} parameter is empty");

            if (new FileInfo(fileName).Extension != ".xml")
                throw new ArgumentException("XML file is required");

            var filePath = $@"{_filesConfig.FilesPath}\{fileName}";
            var fileExists = Path.Exists(filePath);
            if (!fileExists)
                throw new FileNotFoundException($"{nameof(fileName)} : {fileName} doesn't exist");

            var fullPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (string.IsNullOrEmpty(fullPath))
                throw new DirectoryNotFoundException($"Path : {fullPath} doesn't exist");

            var result = _fileReader.ReadXmlFile(fileName);

            Console.WriteLine($"{nameof(FileReader)}.{nameof(ReadXmlFile)} validation passed");

            return result;
        }
    }
}
