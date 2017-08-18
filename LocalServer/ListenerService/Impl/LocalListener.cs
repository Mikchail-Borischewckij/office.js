using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using ListenerService.Common.Impl;

namespace ListenerService.Impl
{
    public class LocalListener : Listener
    {
        public const string LocalListenerEndpoint = "http://localhost:{0}/";
        public const int LocalAddressesListenerPort = 20400;

        protected override void Listen()
        {
            int port = GetListenerPort();
            string prefix = string.Format(LocalListenerEndpoint, port);
            HttpListener listener = new HttpListener();
            try
            {
                listener.Prefixes.Add(prefix);
                listener.Start();
                ConsoleLogger.Log("Listener is started on : {0}", prefix);
            }
            catch (Exception ex)
            {
                ConsoleLogger.Log(ex);
                return;
            }

            while (listener.IsListening)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerResponse response = context.Response;
                try
                {
                    string origin = context.Request.Headers["Origin"];

                    response.Headers.Add("Access-Control-Allow-Origin", origin ?? "*");

                    HttpListenerRequest request = context.Request;
                    ConsoleLogger.Log("Request received form: " + request.UserAgent);
                    response.StatusCode = (int)HttpStatusCode.OK;
                    byte[] buffer = Encoding.ASCII.GetBytes("{\"Status\":\"OK\"}");
                    response.ContentType = "application/json";
                    response.ContentEncoding = Encoding.ASCII;
                    response.ContentLength64 = buffer.Length;
                    response.OutputStream.Write(buffer, 0, buffer.Length);
                    response.OutputStream.Close();
                    response.OutputStream.Close();
                }
                catch (ThreadAbortException ex)
                {
                    listener.Stop();

                    ConsoleLogger.Log(ex);
                }
                catch (Exception ex)
                {
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.OutputStream.Close();

                    ConsoleLogger.Log(ex);
                }
            }
        }

        private int GetListenerPort()
        {
            // Check port for availability.
            return Network.IsAvailablePort(LocalAddressesListenerPort) ? LocalAddressesListenerPort : Network.GetAvailablePort(20400, 20900);
        }
    }
}
