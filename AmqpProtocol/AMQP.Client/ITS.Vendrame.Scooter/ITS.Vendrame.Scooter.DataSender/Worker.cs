using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ITS.Vendrame.Scooter.QueueLibrary.QueueController;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ITS.Vendrame.Scooter.DataSender
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly QueueController _queueController;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _queueController = new QueueController();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _queueController.CheckIfDataIsPresent();
                }catch(Exception e)
                {
                    _logger.LogError("No Internet connection, client will send data as soon as possible.");
                }
                
                Thread.Sleep(30000);
                
            }
        }
    }
}

