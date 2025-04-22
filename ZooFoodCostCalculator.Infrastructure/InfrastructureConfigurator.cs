using Microsoft.Extensions.DependencyInjection;
using ZooFoodCostCalculator.Application;
using ZooFoodCostCalculator.Application.Interfaces;
using ZooFoodCostCalculator.Domain.Interfaces;
using ZooFoodCostCalculator.Infrastructure.IO;
using ZooFoodCostCalculator.Infrastructure.IO.LoggingDecorators;
using ZooFoodCostCalculator.Infrastructure.IO.Validators;
using ZooFoodCostCalculator.Infrastructure.Repositories;
using ZooFoodCostCalculator.Infrastructure.Repositories.LoggingDecorators;
using ZooFoodCostCalculator.Infrastructure.Repositories.Validators;

namespace ZooFoodCostCalculator.Infrastructure
{
    public static class InfrastructureConfigurator
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddApplicationServices();

            services.AddSingleton<IFileReader, FileReader>();
            services.Decorate<IFileReader, FileReaderValidator>();
            services.Decorate<IFileReader, LoggingFileReaderDecorator>();

            services.AddTransient<IAnimalFoodRepository, AnimalFoodRepository>();
            services.Decorate<IAnimalFoodRepository, AnimalFoodRepositoryValidator>();
            services.Decorate<IAnimalFoodRepository, LoggingAnimalFoodRepositoryDecorator>();

            services.AddTransient<IPriceItemRepository, PriceItemRepository>();
            services.Decorate<IPriceItemRepository, PriceItemRepositoryValidator>();
            services.Decorate<IPriceItemRepository, LoggingPriceItemRepositoryDecorator>();

            services.AddTransient<IZooRepository, ZooRepository>();
            services.Decorate<IZooRepository, ZooRepositoryValidator>();
            services.Decorate<IZooRepository, LoggingZooRepositoryDecorator>();

            return services;
        }
    }
}
