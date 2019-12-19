using System.Web.Http;
using Quartz.Batch.Service.Jobs;

namespace Quartz.Batch.Service
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            JobScheduler.Start();
        }
    }
}
