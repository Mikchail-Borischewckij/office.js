using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace ListenerService
{
    public static class Network
    {
        public static bool IsAvailablePort(int port)
        {
            HashSet<int> unavailablePorts = GetUnavailablePorts();

            return !unavailablePorts.Contains(port);
        }

        public static HashSet<int> GetUnavailablePorts()
        {
            IPGlobalProperties globalProperties = IPGlobalProperties.GetIPGlobalProperties();
            IEnumerable<int> ports = globalProperties.GetActiveTcpListeners().Select(activeListener => activeListener.Port);

            return new HashSet<int>(ports);
        }

        public static int GetAvailablePort(int startRange, int endRange)
        {
            int availablePort = 0;
            HashSet<int> unavailablePorts = GetUnavailablePorts();

            for (int currentPort = startRange; currentPort <= endRange; currentPort++)
            {
                if (unavailablePorts.Contains(currentPort)) continue;

                availablePort = currentPort;
                break;
            }

            return availablePort;
        }
    }
}
