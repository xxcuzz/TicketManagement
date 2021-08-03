using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web.Configuration;
using System.Web.Mvc;

namespace ThirdPartyEventEditor.Filters
{
    public class LoggingFilter : ActionFilterAttribute
    {
        private readonly Stopwatch _stopWatch = new Stopwatch();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _stopWatch.Reset();
            _stopWatch.Start();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _stopWatch.Stop();
            var executionTime = _stopWatch.ElapsedMilliseconds;

            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];

            Log(controllerName, actionName, executionTime, GetLoggerPath());
        }

        public void Log(string controllerName, string actionName, long executionTime, string path)
        {
            var sb = new StringBuilder();
            sb.AppendLine(DateTime.Now.ToString("F"));
            sb.AppendLine("Controller " + "\"" + controllerName + "\"");
            sb.AppendLine("Action " + "\"" + actionName + "\"");
            sb.AppendLine("Time: " + executionTime);
            sb.AppendLine("------------------------------------------------------------" + Environment.NewLine);
            File.AppendAllText(path, sb.ToString());
        }

        private string GetLoggerPath()
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var loggerFileName = WebConfigurationManager.AppSettings["LoggerName"];
            return currentDirectory + @"\App_Data\" + loggerFileName + ".txt";
        }
    }
}