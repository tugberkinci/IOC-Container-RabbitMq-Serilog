using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Serilog;

using Microsoft.Extensions.Logging;
using RabbitListener.Data.Services.Abstract;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;
using Serilog.Events;
using System;
using Serilog.Templates;

namespace RabbitListener
{
    public class Program
    {
        
        static void Main(string[] args)
        {
            var configuration = Configure();
            IoC.ConfigureServices(configuration);
            ConfigureLogger();

            var service = IoC.Services.GetService<IRabbitConsumerService>();
            
            //lag for given service name 
            service.Consume("RabbitListener");

           
        }

        public static IConfiguration Configure()
        {
            //appsetting configuration
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            return builder.Build();
        }

        private static void ConfigureLogger()
        {
            //serilog configuration
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(new ExpressionTemplate("{@mt}\n"), "../../../logs/log.json")
                .CreateLogger();

            var loggerFactory = IoC.Services.GetRequiredService<ILoggerFactory>();
            loggerFactory.AddSerilog();
        }

    }
}
