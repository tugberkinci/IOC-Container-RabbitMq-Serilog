using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitListener.Dto.ConfigurationModels
{
    /// <summary>
    /// Rabbitmq configuration handler
    /// </summary>
    public class RabbitConfiguration
    {
        public string? Host { get; set; }
        public string? Port { get; set; }
        public string? QueueName { get; set; }
    }
}
