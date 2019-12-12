using log4net;
using Topshelf;
using Topshelf.Autofac;

namespace Quartz.Batch.WinService
{
    class Program
    {
        private static readonly ILog txtLog = LogManager.GetLogger("");
        private static readonly ILog dataBaseLog = LogManager.GetLogger("DBLog");
        static void Main(string[] args)
        {
            txtLog.Info("Batch.WinService Running");
            var container = Bootstrapper.BuildContainer();
            var rc = HostFactory.Run(x =>
            {
                x.UseAutofacContainer(container);
                x.Service<SchedulerService>(s =>
                {
                    s.ConstructUsingAutofacContainer();
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.SetDescription("Sample Quartz.Batch.WinService");
                x.SetDisplayName("Quartz.Batch.WinService");
                x.SetServiceName("Quartz.Batch.WinService");
                x.RunAsLocalSystem();
                x.StartAutomatically();
            });

            

        }
    }
}
