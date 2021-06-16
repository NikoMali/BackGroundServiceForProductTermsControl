using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackGroundServiceForProductTermsControl.Jobs
{
    public class HelloWorldJob : IJob
    {
        private readonly ILogger<HelloWorldJob> _logger;
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        public IScheduler Scheduler { get; set; }
        public HelloWorldJob(
            ILogger<HelloWorldJob> logger,
           ISchedulerFactory schedulerFactory,
            IJobFactory jobFactory)
        {
            _schedulerFactory = schedulerFactory;

            _jobFactory = jobFactory;
            _logger = logger;
        }
        public  Task Execute(IJobExecutionContext context)
        {
           
            Scheduler = _schedulerFactory.GetScheduler().GetAwaiter().GetResult();
            Scheduler.JobFactory = _jobFactory;

            var p = Scheduler.GetJobDetail(new JobKey("HelloWorldJob","GROUP"));
            _logger.LogWarning("HelloWorldJob");
            return Task.CompletedTask;
        }
    }
}
