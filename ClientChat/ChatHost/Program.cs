using System;
using System.ServiceModel;

namespace ChatHost
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // creating host
            using (var host = new ServiceHost(typeof(ClientChat.ServiceChat)))
            {
                host.Open();
                Console.WriteLine("Host started");
                Console.ReadLine();
            }
        }
    }
}
