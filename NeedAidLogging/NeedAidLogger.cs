using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NeedAidLogging
{
    public class NeedAidLogger : ILogger
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public string ApplicationName { get; set; }

        public Logger DefaultLogger
        {
            get
            {
                return logger;
            }
        }

        public NeedAidLogger()
        {
            if (string.IsNullOrEmpty(ApplicationName))
            {
                ApplicationName = string.Empty;
            }
        }

        public virtual void Debug(string message, [CallerMemberName] string callerName = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            var defaultMessageHeader = GetDefaultMessageHeader(callerName, callerLineNumber);
            logger.Debug($"{defaultMessageHeader} >> {message}");
        }

        public virtual void Error(string message, [CallerMemberName] string callerName = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            var defaultMessageHeader = GetDefaultMessageHeader(callerName, callerLineNumber);
            logger.Error($"{defaultMessageHeader} >> {message}");
        }

        public virtual void ErrorException(string message, Exception exception, [CallerMemberName] string callerName = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            var defaultMessageHeader = GetDefaultMessageHeader(callerName, callerLineNumber);
            logger.Error($"{defaultMessageHeader} >> {message}", exception);
        }

        public virtual void Fatal(string message, [CallerMemberName] string callerName = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            var defaultMessageHeader = GetDefaultMessageHeader(callerName, callerLineNumber);
            logger.Fatal($"{defaultMessageHeader} >> {message}");
        }

        public virtual void Fatal(string message, Exception exception, [CallerMemberName] string callerName = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            var defaultMessageHeader = GetDefaultMessageHeader(callerName, callerLineNumber);
            logger.Fatal($"{defaultMessageHeader} >> {message}", exception);
        }

        public virtual void Info(string message, [CallerMemberName] string callerName = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            var defaultMessageHeader = GetDefaultMessageHeader(callerName, callerLineNumber);
            logger.Info($"{defaultMessageHeader} >> {message}");
        }

        public virtual void Trace(string message, [CallerMemberName] string callerName = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            var defaultMessageHeader = GetDefaultMessageHeader(callerName, callerLineNumber);
            logger.Trace($"{defaultMessageHeader} >> {message}");
        }

        public virtual void Warn(string message, [CallerMemberName] string callerName = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            var defaultMessageHeader = GetDefaultMessageHeader(callerName, callerLineNumber);
            logger.Warn($"{defaultMessageHeader} >> {message}");
        }

        private string GetDefaultMessageHeader(string callerName = "", int callerLineNumber = 0)
        {
            var ipAddress = CommonWebUtility.GetRequestIp(HttpContext.Current?.Request) ?? string.Empty;
            var hostName = CommonWebUtility.GetHostname(ipAddress) ?? string.Empty;

            return $"IP:{ipAddress}|Host:{hostName}|AppName:{ApplicationName} >> {callerName} >> {callerLineNumber} ";
        }
    }
}
