using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ZooFoodCostCalculator.Application.CommandsHandlers;
using ZooFoodCostCalculator.Application.Factories;
using ZooFoodCostCalculator.Application.Factories.LoggingDecorators;
using ZooFoodCostCalculator.Application.Factories.Validators;
using ZooFoodCostCalculator.Application.Interfaces;
using ZooFoodCostCalculator.Application.Services;
using ZooFoodCostCalculator.Application.Services.LoggingDecorators;
using ZooFoodCostCalculator.Application.Services.ValidationDecorators;
using ZooFoodCostCalculator.Application.Strategies.FoodCostCalculatorStrategies;
using ZooFoodCostCalculator.Application.Strategies.FoodCostCalculatorStrategies.Validators;
using ZooFoodCostCalculator.Domain;

namespace ZooFoodCostCalculator.Application
{
    public static class ApplicationConfigurator
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddDomainServices();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(ApplicationConfigurator).Assembly);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(CommandHandlerLogger<,>));
            });

            services.AddTransient<IZooApplicationService, ZooApplicationService>();
            services.Decorate<IZooApplicationService, ZooApplicationServiceValidator>();
            services.Decorate<IZooApplicationService, LoggingZooApplictionServiceDecorator>();

            services.AddTransient<IFoodCostStrategyFactory, FoodCostStrategyFactory>();
            services.Decorate<IFoodCostStrategyFactory, FoodCostStrategyFactoryValidator>();
            services.Decorate<IFoodCostStrategyFactory, LoggingFoodCostStrategyFactoryDecorator>();

            services.AddKeyedTransient<IDietTypeCostCalculatorStrategy, CarnivoreCostCalculatorStrategy>(nameof(CarnivoreCostCalculatorStrategy));
            services.AddKeyedTransient<IDietTypeCostCalculatorStrategy, HerbivoreCostCalculatorStrategy>(nameof(HerbivoreCostCalculatorStrategy));
            services.AddKeyedTransient<IDietTypeCostCalculatorStrategy, OmnivoreCostCalculatorStrategy>(nameof(OmnivoreCostCalculatorStrategy));

            services.AddKeyedTransient<ICostCalculatorStrategyValidator, OmnivoreCostCalculatorStrategyValidator>(nameof(OmnivoreCostCalculatorStrategyValidator));
            services.AddKeyedTransient<ICostCalculatorStrategyValidator, HerbivoreCostCalculatorStrategyValidator>(nameof(HerbivoreCostCalculatorStrategyValidator));
            services.AddKeyedTransient<ICostCalculatorStrategyValidator, CarnivoreCostCalculatorStrategyValidator>(nameof(CarnivoreCostCalculatorStrategyValidator));

            return services;
        }
    }
}
