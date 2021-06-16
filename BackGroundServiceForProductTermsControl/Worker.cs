using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BackGroundServiceForProductTermsControl
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private HttpClient httpClient;
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            httpClient = new HttpClient();
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            httpClient.Dispose();
            return base.StopAsync(cancellationToken);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var count = 0;
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    //httpClient = new HttpClient();
                    var result = await httpClient.GetAsync("http://161.97.167.22/swagger/index.html");
                    if (result.IsSuccessStatusCode)
                    {
                        _logger.LogWarning("The Website is Up.Status Code {StatusCode} =" + result.StatusCode);
                    }
                    else
                    {
                        _logger.LogError("The Website is Down.Status Code {StatusCode}", result.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("The Website is Down {0}.", ex.Message);
                }
                finally
                {
                    count++;
                    /*if (count == 3)
                    {
                        await StopAsync(stoppingToken);

                    }*/
                    await Task.Delay(1000 * 5, stoppingToken);
                }

                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);  

            }
        }
    }
}
