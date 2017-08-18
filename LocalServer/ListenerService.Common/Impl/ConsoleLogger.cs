using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ListenerService.Common.Impl
{
    public class ConsoleLogger
    {
        private static readonly ILoggerInternal InternalLogger = new FileLogger();

        private const string MessagePattern = "{0} -- Message: {1}";
        private const string DateTimeStandartFormat = "yy-MM-dd HH:mm:ss.fff";
        private const string ErrorPattern = "{0} -- An exception was thrown with the following information: {1}";

        /// <summary>
        /// Logs the specified format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public static void Log(string format, params object[] args)
        {
            string formatMessage = string.Format(format, args);
            Log(formatMessage);
        }

        /// <summary>
        /// Logs the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        public static void Log(string content)
        {
            List<string> args = new List<string>
            {
                content
            };

            string logInfo = FormatMessage(MessagePattern, args);
            Console.WriteLine(logInfo);
            InternalLogger.Log(logInfo);
        }
        
        /// <summary>
        /// Logs the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public static void Log(Exception ex)
        {
            List<string> args = new List<string>
            {
                ex.ToString()
            };

            string logInfo = FormatMessage(ErrorPattern, args);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(logInfo);
            InternalLogger.Log(logInfo);
        }
        
        /// <summary>
        /// Formats the message.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        private static string FormatMessage(string pattern, List<string> args)
        {
            string dateTime = DateTime.Now.ToString(DateTimeStandartFormat);
            args.Insert(0, dateTime);

            return string.Format(pattern, args.ToArray());
        }
    }
}
