using System;
using System.Threading;
using System.Threading.Tasks;

namespace Quartz.Batch.WinService.Jobs
{
    internal class SampleJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return new Task(Action, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
        }

        private void Action(object obj)
        {
            Console.WriteLine("Task={0}, obj={1}, Thread={2}",
                Task.CurrentId, obj,
                Thread.CurrentThread.ManagedThreadId);
        }
    }
}
