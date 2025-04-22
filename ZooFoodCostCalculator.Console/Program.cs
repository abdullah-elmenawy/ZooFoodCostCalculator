using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ZooFoodCostCalculator.Common;
using ZooFoodCostCalculator.Console;
using ZooFoodCostCalculator.Infrastructure;
using log4net;
using log4net.Config;
using System.Reflection;
using ZooFoodCostCalculator.Common.Options;

var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                config.AddCommandLine(args);
            })
           .ConfigureServices((hostContext, services) =>
           {
               services.AddLogging(log =>
               {
                   log.AddLog4Net();
               });
               var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
               XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

               var configurationRoot = hostContext.Configuration;

               services.Configure<FilesOptions>(
                   configurationRoot.GetSection(FilesOptions.FilesSectionName));

               services.AddTransient<ZooApp>();
               services.AddCommonServices();
               services.AddInfrastructureServices();
           }).Build();

Console.ForegroundColor = ConsoleColor.Yellow;
var logger = host.Services.GetRequiredService<ILogger<Program>>();

AppDomain.CurrentDomain.UnhandledException += (sender, exception) =>
{
    var ex = (Exception)exception.ExceptionObject;
    logger.LogCritical(ex, "Unhandled exception occurred.");
    Console.WriteLine("An error ocurred. Please restart the application");
};

var app = host.Services.GetRequiredService<ZooApp>();
await app.RunAsync();