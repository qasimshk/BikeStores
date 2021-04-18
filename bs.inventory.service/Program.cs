using bs.component.core.Extensions;
using bs.inventory.infrastructure.Persistence.Context;
using bs.inventory.service.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace bs.inventory.service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureAppConfiguration(config => config.AddUserSecrets(Assembly.GetExecutingAssembly()))
                .ConfigureHostConfiguration(cfg =>
                {
                    cfg.SetBasePath(Directory.GetCurrentDirectory());
                    cfg.AddJsonFile("appsettings.json", true, true);
                    cfg.AddJsonFile($"appsettings.{GetValueByKey(args, "environment")}.json", true, true);
                    cfg.AddEnvironmentVariables().Build();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services
                        .AddServiceConfiguration(hostContext.Configuration)
                        .AddHandler()
                        .AddApplicationLogging(hostContext.Configuration)
                        .AddApplicationDbContext<InventoryDbContext>(hostContext.Configuration)
                        .AddEventBus(hostContext.Configuration)
                        .AddApplicationModules()
                        .AddHostedService<Worker>();
                });

        private static string GetValueByKey(IEnumerable<string> args, string key)
        {
            return args.Single(x => x.Contains(key)).Split('=').Last();
        }
    }
}
