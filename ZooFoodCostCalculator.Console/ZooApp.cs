using ZooFoodCostCalculator.Application.Interfaces;

namespace ZooFoodCostCalculator.Console
{
    public class ZooApp(IZooApplicationService zooApplicationService)
    {
        public async Task RunAsync()
        {
            System.Console.WriteLine("Welcome to Zoo Food Cost Calculator");
            var totalCost = await zooApplicationService.CalculateFoodCost();

            System.Console.WriteLine();
            System.Console.ForegroundColor = ConsoleColor.DarkYellow;
         
            System.Console.WriteLine("Total food cost for the Zoo is : {0}$", totalCost);
            
            System.Console.WriteLine();
            System.Console.ResetColor();

        }
    }
}
