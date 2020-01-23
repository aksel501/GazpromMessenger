using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace MSMQChecker
{
    class Program
    {
        private

        static void Main(string[] args)
        {
            Console.WriteLine("Application Started");
            Console.WriteLine("Building Configuration");
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            Configuration.configuration = configuration;
            Console.WriteLine("Running Main Algorithm");
            
            ApplicationRunner.Run();
        }

    }
}
