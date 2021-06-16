using Microsoft.AspNetCore.Mvc;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackGroundServiceForProductTermsControl.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JobsController : Controller
    {
        
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        public IScheduler Scheduler { get; set; }
        public JobsController(
          
            ISchedulerFactory schedulerFactory,
            IJobFactory jobFactory)
        {
            _schedulerFactory = schedulerFactory;
            _jobFactory = jobFactory;

            Scheduler = _schedulerFactory.GetScheduler().GetAwaiter().GetResult();
            Scheduler.JobFactory = _jobFactory;

        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(new {proccess = "Hello" });
        }
        [HttpGet("HelloWorldJobPause")]
        public IActionResult HelloWorldJobPause()
        {
            
            Scheduler.PauseJob(new JobKey("HelloWorldJob"));
            Scheduler.PauseTrigger(new TriggerKey("HelloWorldJob-trigger"));
            return Ok(new { proccess = "HelloWorldJobPause" });
        }
        [HttpGet("HelloWorldJobResume")]
        public IActionResult HelloWorldJobResume()
        {
            Scheduler.ResumeJob(new JobKey("HelloWorldJob"));
            //Scheduler.RescheduleJob(new TriggerKey("HelloWorldJob-trigger"), Scheduler.GetTriggersOfJob(new JobKey("HelloWorldJob")).Result.First());
            Scheduler.ResumeTrigger(new TriggerKey("HelloWorldJob-trigger"));
            return Ok(new { proccess = "HelloWorldJobResume" });
        }
        [HttpGet("HelloWorldJobDetail")]
        public IActionResult HelloWorldJobDetail()
        {

            Scheduler.GetJobDetail(new JobKey("HelloWorldJob"));
            return Ok(new { Scheduler.GetJobDetail(new JobKey("HelloWorldJob")).Result });
        }

    }
}
