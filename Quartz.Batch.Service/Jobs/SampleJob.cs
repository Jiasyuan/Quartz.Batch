using System;
using System.Threading.Tasks;

namespace Quartz.Batch.Service.Jobs
{
    public class SampleJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync($"SampleJob:{DateTime.Now} ");
        }
    }
}