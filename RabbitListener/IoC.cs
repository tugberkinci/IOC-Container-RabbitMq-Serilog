using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RabbitListener.Data.Services.Abstract;
using RabbitListener.Data.Services.Concrete;
using Microsoft.Extensions.Configuration;
using RabbitListener.Dto.ConfigurationModels;

namespace RabbitListener
{
    public static class IoC
    {
        public static IServiceProvider Services { get; private set; }

        public static void ConfigureServices(IConfiguration configuration)
        {
            IServiceCollection services = new ServiceCollection();

            // Inject consume service
            services.AddSingleton<IRabbitConsumerService, RabbitConsumerService>();

            // Rabbitmq configuration
            services.Configure<RabbitConfiguration>(configuration.GetSection("RabbitConfiguration"));

            // Add required services for  logging
            services.AddOptions();
            services.AddLogging();

            Services = services.BuildServiceProvider();
        }
    }
}
