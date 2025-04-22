using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using ZooFoodCostCalculator.Application.Interfaces;
using ZooFoodCostCalculator.Common.Options;

namespace ZooFoodCostCalculator.Infrastructure.IO
{
    public class FileReader : IFileReader
    {
        private readonly FilesOptions _filesConfig;
        public FileReader(IOptions<FilesOptions> filesConfig)
        {
            _filesConfig = filesConfig.Value;
        }
        public string[] ReadFileAsLines(string fileName)
        {
            var filePath = $@"{_filesConfig.FilesPath}\{fileName}";

            var fullPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            
            string combinedPath = Path.Combine(fullPath, filePath);
            string[] fileLines = File.ReadAllLines(combinedPath);
            return fileLines;
        }
        public List<string> ReadXmlFile(string fileName)
        {
            var filePath = $@"{_filesConfig.FilesPath}\{fileName}";
            var fullPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string combinedPath = Path.Combine(fullPath, filePath);

            var document = XDocument.Load(combinedPath);

            List<string> lines = new List<string>();
            foreach (var group in document.Root.Elements())
            {
                foreach (var item in group.Elements())
                {
                    StringBuilder builder = new ();
                    builder.Append(item.Name);
                    builder.Append(',');
                    var values = item.Attributes().Select(att => att.Value + ",").ToList();
                    values.ForEach(c => builder.Append(c));
                    lines.Add(builder.ToString());
                }
            }

            return lines;
        }
    }
}
