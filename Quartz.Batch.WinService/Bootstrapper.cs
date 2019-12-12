using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Autofac;
using Autofac.Extras.Quartz;
using Quartz.Batch.WinService.Jobs;

namespace Quartz.Batch.WinService
{
    public static class Bootstrapper
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<SchedulerService>();

            var schedulerConfig = new NameValueCollection
            {
                { "quartz.scheduler.instanceName", "MyScheduler" },
                { "quartz.jobStore.type", "Quartz.Simple.RAMJobStore, Quartz" },
                { "quartz.threadPool.threadCount", "3" }
            };

            builder.RegisterModule(new QuartzAutofacFactoryModule
            {
                ConfigurationProvider = c => schedulerConfig
            });

            builder.RegisterModule(new QuartzAutofacJobsModule(typeof(SampleJob_WriteDB).Assembly));

            var connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            builder
                .RegisterType<SqlConnection>()
                .WithParameter("connectionString", connectionString)
                .As<IDbConnection>()
                .InstancePerMatchingLifetimeScope(QuartzAutofacFactoryModule.LifetimeScopeName);

            // Other registrations

            var container = builder.Build();
            return container;
        }
    }
}