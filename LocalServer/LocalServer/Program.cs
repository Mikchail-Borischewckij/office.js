using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ListenerService;
using ListenerService.Impl;

namespace LocalServer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IListener listener = new LocalListener())
            {
                listener.Run();
                Console.ReadKey();
            }
        }
    }
}
