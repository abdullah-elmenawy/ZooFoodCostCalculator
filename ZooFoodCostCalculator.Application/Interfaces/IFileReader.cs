namespace ZooFoodCostCalculator.Application.Interfaces
{
    public interface IFileReader
    {
        string[] ReadFileAsLines(string fileName);
        List<string> ReadXmlFile(string fileName);
    }
}
