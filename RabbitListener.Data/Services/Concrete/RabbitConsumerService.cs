using RabbitListener.Data.Repository.Concrete;
using RabbitListener.Data.Services.Abstract;
using RabbitListener.Data.Utilities.Response;
using RabbitListener.Dto.Concrete;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitListener.Dto.ConfigurationModels;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Options;
using Serilog;
using RabbitListener.Dto.Model;

namespace RabbitListener.Data.Services.Concrete
{
    /// <summary>
    /// Rabbit consumer service
    /// </summary>
    public class RabbitConsumerService : BaseRepository<RabbitDto>, IRabbitConsumerService
    {
        private readonly RabbitConfiguration _rabbitConfiguration;
        
        public RabbitConsumerService(
            IOptions<RabbitConfiguration> rabbitConfiguration
            )
        {
            _rabbitConfiguration = rabbitConfiguration.Value;
        }


        /// <summary>
        /// Consumer
        /// </summary>
        /// <param name="serviceName"></param>
        public void Consume(string serviceName)
        {
            #region Validations
            var rabbitHost = _rabbitConfiguration.Host;
            if (String.IsNullOrEmpty(rabbitHost))
            {
                Console.WriteLine("There is no host configured for rabbitMq server.");
                return;
            }

            var queueName = _rabbitConfiguration.QueueName;
            if (String.IsNullOrEmpty(rabbitHost))
            {
                Console.WriteLine("There is no queue name configured.");
                return;
            }

            if(String.IsNullOrEmpty(serviceName)) 
            {
                Console.WriteLine("Service name could not be null or empty.");
                return;
            }
            #endregion

            //var factory = new ConnectionFactory() { HostName = "localhost" };
            var factory = new ConnectionFactory() { HostName = rabbitHost };
            using (HttpClient client = new HttpClient())
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var item = JsonConvert.DeserializeObject<RabbitDto>(message);

                    var statusCode = await RequestHandlerService.HeadAsync(client, item.Url);

                    Console.WriteLine($"URL={item.Url}");
                    Console.WriteLine($"Status Code={statusCode}");

                    var logModel = RabbitLogModel.Instance(serviceName, item.Url, statusCode);
                    var logMessage = JsonConvert.SerializeObject(logModel);
                    Log.Information(logMessage);

                    Thread.Sleep(1000);
                };
                channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

                Console.WriteLine("No url request yet.");
                Console.ReadLine();
               
            }
        }
    }
}
