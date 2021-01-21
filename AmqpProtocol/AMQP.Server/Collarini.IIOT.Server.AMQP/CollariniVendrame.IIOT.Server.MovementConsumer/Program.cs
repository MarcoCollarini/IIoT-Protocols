using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CollariniVendrame.IIOT.Server.ServiceStorageSQL;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CollariniVendrame.IIOT.Server.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddSingleton<IDetectionRepository, DetectionRepository>();
                });
    }
}
