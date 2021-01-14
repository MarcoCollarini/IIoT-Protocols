using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CollariniVendrame.IIOT.Server.API.ServiceStorageAzure;
using CollariniVendrame.IIOT.Server.ServiceStorageSQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CollariniVendrame.IIOT.Server.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //Storage Azure
            services.AddSingleton<ServiceStorageAzure.ISensorRepository, ServiceStorageAzure.SensorRepository>();
            services.AddSingleton<ServiceStorageAzure.IScooterRepository, ServiceStorageAzure.ScooterRepository>();

            //Storage SQL
            services.AddSingleton<ServiceStorageSQL.IDetectionRepository, ServiceStorageSQL.DetectionRepository>();
            services.AddSingleton<ServiceStorageSQL.ISensorRepository, ServiceStorageSQL.SensorRepository>();
            services.AddSingleton<ServiceStorageSQL.IScooterRepository, ServiceStorageSQL.ScooterRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
