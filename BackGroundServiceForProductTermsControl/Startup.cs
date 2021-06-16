using BackGroundServiceForProductTermsControl.Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackGroundServiceForProductTermsControl
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        private readonly IConfiguration _configuration;
        private IScheduler scheduler;
        public Startup(IConfiguration configuration)
        {
            
            _configuration = configuration;

        }
        public void ConfigureServices(IServiceCollection services)
        {
            Serilogging.SerilogInitial(_configuration);
            services.AddControllers();

            services.AddHostedService<Worker>();

            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionScopedJobFactory();

                // Register the job, loading the schedule from configuration
                q.AddJobAndTrigger<HelloWorldJob>(_configuration);
                q.AddJobAndTrigger<SecondJob>(_configuration);
            });

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);


            scheduler = StdSchedulerFactory.GetDefaultScheduler().GetAwaiter().GetResult();
            services.AddSingleton(scheduler);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            scheduler = StdSchedulerFactory.GetDefaultScheduler().GetAwaiter().GetResult();
            //ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            /*var scheduler =  StdSchedulerFactory.GetDefaultScheduler()
                                                    .ConfigureAwait(false)
                                                    .GetAwaiter()
                                                    .GetResult();
            var job = scheduler.GetJobDetail(new JobKey(typeof(HelloWorldJob).Name));
            var job1 = scheduler.GetJobDetail(new JobKey(typeof(HelloWorldJob).Name + "-trigger"));*/

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
