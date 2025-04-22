using Microsoft.Extensions.DependencyInjection;

namespace ZooFoodCostCalculator.Common
{
    public static class CommonConfigurator
    {
        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
