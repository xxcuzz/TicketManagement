using System;
using System.IO;
using System.Text;
using System.Web.Configuration;
using System.Web.Mvc;

namespace ThirdPartyEventEditor.Filters
{
    public class ExceptionFilter : ActionFilterAttribute, IExceptionFilter
    {
        private static readonly string _loggerPath = GetLoggerPath();

        private static string GetLoggerPath()
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var loggerFileName = WebConfigurationManager.AppSettings["LoggerName"];
            return $@"{currentDirectory}\App_Data\{loggerFileName}.txt";
        }

        public void OnException(ExceptionContext filterContext)
        {
            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];
            var exception = filterContext.Exception;

            var model = new HandleErrorInfo(exception, controllerName, actionName);

            Log(exception, controllerName, actionName);

            filterContext.ExceptionHandled = true;
            var view = new ViewResult();
            view.ViewName = "Exception";
            view.ViewData = new ViewDataDictionary();
            view.ViewData.Model = model;

            view.ExecuteResult(filterContext);
        }

        public void Log(Exception exception, string controllerName, string actionName)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Controller: {controllerName}");
            sb.AppendLine($"Action: {actionName}");
            GetExceptionInfo(exception, sb);
            sb.AppendLine("------------------------------------------------------------" + Environment.NewLine);
            File.AppendAllText(_loggerPath, sb.ToString());
        }

        private void GetExceptionInfo(Exception exception, StringBuilder sb)
        {
            sb.AppendLine(exception.GetType().ToString());
            sb.AppendLine(exception.Message);
            sb.AppendLine("Stack Trace: ");
            sb.AppendLine(exception.StackTrace);

            if (exception.InnerException != null)
            {
                sb.AppendLine("InnerException: ");
                GetExceptionInfo(exception.InnerException, sb);
            }
        }
    }
}
