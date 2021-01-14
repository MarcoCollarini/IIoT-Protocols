using CollariniVendrame.IIOT.Server.AzureFunction.Queue.ServiceStorage;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
[assembly: FunctionsStartup(typeof(CollariniVendrame.IIOT.Server.AzureFunction.Queue.Startup))]
namespace CollariniVendrame.IIOT.Server.AzureFunction.Queue
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
           builder.Services.AddSingleton<IDetectionRepository, DetectionRepository>();
           builder.Services.AddSingleton<IInfluxDbRepository, InfluxDbRepository>();
        }
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            FunctionsHostBuilderContext context = builder.GetContext();

            builder.ConfigurationBuilder
                .AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.json"), optional: true, reloadOnChange: false)
                .AddJsonFile(Path.Combine(context.ApplicationRootPath, $"appsettings.{context.EnvironmentName}.json"), optional: true, reloadOnChange: false)
                .AddEnvironmentVariables();
        }
    }
}

