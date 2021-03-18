using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackGroundServiceForProductTermsControl.Jobs
{
    public class SecondJob : IJob
    {
        private readonly ILogger<SecondJob> _logger;
        public SecondJob(ILogger<SecondJob> logger)
        {
            _logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("SecondJob");
            return Task.CompletedTask;
        }
    }
}
