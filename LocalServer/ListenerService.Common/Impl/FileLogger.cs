using System.IO;
using System.Threading;

namespace ListenerService.Common.Impl
{
    internal class FileLogger : ILoggerInternal
    {
        private const string MutexName = @"Global\DaemonConnectorLogFileSyncMutex";
        private readonly Mutex _mutex = new Mutex(false, MutexName);

        /// <summary>
        /// Logs the specified information.
        /// </summary>
        /// <param name="information">The information.</param>
        public void Log(string information)
        {
            string directoryPath = Path.Combine("C:/", "LogFiles");
            CheckForDirectoryExitance(directoryPath);
            string fullPath = Path.Combine(directoryPath, "Listener.log");

            try
            {
                _mutex.WaitOne();
                File.AppendAllText(fullPath, information);
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }

        private static void CheckForDirectoryExitance(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
