using System;
using System.Net;
using System.Threading;
using ListenerService.Common.Impl;

namespace ListenerService.Impl
{
    public abstract class Listener : IListener
    {
        protected string ListenerName;

        private bool _disposed;
        private Thread _listenerThread;

        public void Run()
        {
            if (!HttpListener.IsSupported)
            {
                ConsoleLogger.Log(new ApplicationException($"HttpListener is not supported on {Environment.OSVersion}."));
                return;
            }

            _listenerThread = new Thread(Listen);
            _listenerThread.Start();
        }

        /// <summary>
        /// Listens this instance.
        /// </summary>
        protected abstract void Listen();
        
        #region Disposable pattern

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (_listenerThread != null)
                {
                    _listenerThread.Abort();
                    _listenerThread = null;
                }
                _disposed = true;
            }
        }

        #endregion
    }
}
