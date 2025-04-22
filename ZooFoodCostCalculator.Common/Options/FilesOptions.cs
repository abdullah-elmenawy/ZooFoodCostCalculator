namespace ZooFoodCostCalculator.Common.Options
{
    public class FilesOptions
    {
        public static readonly string FilesSectionName = "Files";
        public string FilesPath { get; set; } = string.Empty;
        public string AnimalFoodFileName { get; set; } = string.Empty;
        public string PricesFileName { get; set; } = string.Empty;
        public string ZooFileName { get; set; } = string.Empty;
    }
}
