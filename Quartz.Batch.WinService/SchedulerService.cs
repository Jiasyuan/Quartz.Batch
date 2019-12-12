using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz.Batch.WinService.Jobs;

namespace Quartz.Batch.WinService
{
    public class SchedulerService
    {
        private readonly IScheduler _scheduler;

        public SchedulerService(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public void Start()
        {
            ScheduleJobs();
            _scheduler.Start().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private void ScheduleJobs()
        {
            ScheduleJobWithCronSchedule<SampleJob>("");
            ScheduleJobWithCronSchedule<SampleJob_WriteDB>("");
        }

        private void ScheduleJobWithCronSchedule<T>(string cronSchedule) where T : IJob
        {
            var jobName = typeof(T).Name;
            var job = JobBuilder
                .Create<T>()
                .WithIdentity(jobName, $"{jobName}-Group")
                .Build();

            var cronTrigger = TriggerBuilder
                .Create()
                .WithIdentity($"{jobName}-Trigger")
                .StartNow()
                .WithCronSchedule(cronSchedule)
                .ForJob(job)
                .Build();

            _scheduler.ScheduleJob(cronTrigger);
        }

        public void Stop()
        {
            _scheduler.Shutdown().ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
