using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

namespace Quartz.Batch.WinService
{
    class Program
    {
        private static readonly ILog txtLog = LogManager.GetLogger("");
        private static readonly ILog dataBaseLog = LogManager.GetLogger("DBLog");
        static void Main(string[] args)
        {
            txtLog.Info("Batch.WinService Running");

        }
    }
}
