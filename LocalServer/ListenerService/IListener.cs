using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListenerService
{
    public interface IListener : IDisposable
    {
        /// <summary>
        /// Runs this instance.
        /// </summary>
        void Run();
    }
}
