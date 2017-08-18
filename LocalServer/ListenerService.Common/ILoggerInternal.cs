using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListenerService.Common
{
    public interface ILoggerInternal
    {
        /// <summary>
        /// Logs the specified information.
        /// </summary>
        /// <param name="information">The information.</param>
        void Log(string information);
    }
}
