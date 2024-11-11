using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace Selenium_FirstTask.Utils
{
    internal class Logger
    {
        static Logger()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }
        public static void Info(string message)
        {
            Log.Information(message);
        }
        public static void Error(string message, Exception exception)
        {
            Log.Error(message, exception);
        }
    }
}
