using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZooFoodCostCalculator.Application.Interfaces;

namespace ZooFoodCostCalculator.Infrastructure.IO.LoggingDecorators
{
    public class LoggingFileReaderDecorator(
        IFileReader fileReader,
        ILogger<LoggingFileReaderDecorator> logger
        ) : IFileReader
    {
        public string[] ReadFileAsLines(string fileName)
        {
            logger.LogInformation($"[START] {nameof(FileReader)}.{nameof(ReadFileAsLines)} started executing with input : {fileName}");
            var result = fileReader.ReadFileAsLines(fileName);
            logger.LogInformation($"[END] {nameof(FileReader)} . {nameof(ReadFileAsLines)} finished executing with result : {JsonSerializer.Serialize(result)}");
            return result;
        }

        public List<string> ReadXmlFile(string fileName)
        {
            logger.LogInformation($"[START] {nameof(FileReader)}.{nameof(ReadXmlFile)} started executing with input : {fileName}");
            var result = fileReader.ReadXmlFile(fileName);
            logger.LogInformation($"[END] {nameof(FileReader)} . {nameof(ReadXmlFile)} finished executing with result : {JsonSerializer.Serialize(result)}");
            return result;
        }
    }
}
