using System;
using System.Net;

namespace GraphApiSamples
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //certificate validation for callback
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            var graphRequest = new GraphApiRequests(new ConsoleLogger());
            graphRequest.Initialize();
            graphRequest.CreateNewUserAsync("Test").Wait();

            Console.Read();
        }
    }
}
