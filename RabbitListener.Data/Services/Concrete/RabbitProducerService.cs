using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitListener.Data.Repository.Concrete;
using RabbitListener.Data.Services.Abstract;
using RabbitListener.Data.Utilities.Response;
using RabbitListener.Dto.Concrete;
using RabbitListener.Dto.ConfigurationModels;
using RabbitListener.Helper.Extension;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitListener.Data.Services.Concrete
{
    public class RabbitProducerService : BaseRepository<RabbitDto>, IRabbitProducerService
    {
        private readonly RabbitConfiguration _rabbitConfiguration;

        public RabbitProducerService(
            IOptions<RabbitConfiguration> rabbitConfiguration
            )
        {
            _rabbitConfiguration = rabbitConfiguration.Value;
        }

        public async Task<ServiceResponse<bool>> ProduceAsync(RabbitDto item)
        {
            #region validations
            var rabbitHost = _rabbitConfiguration.Host;
            if (String.IsNullOrEmpty(rabbitHost))
                return ServiceResponse<bool>.Failed(new ServiceError
                {
                    Message = "There is no host configured for RabbitMq server"
                });

            var queueName = _rabbitConfiguration.QueueName;
            if (String.IsNullOrEmpty(rabbitHost))
                return ServiceResponse<bool>.Failed(new ServiceError
                {
                    Message = "There is no queue name configured."
                });

            var validateUrl = item.Url.ValidateUrl();
            if (!validateUrl)
                return ServiceResponse<bool>.Failed(new ServiceError
                {
                    Message = "Url is not valid"
                });
            #endregion

            try
            {
               
                var factory = new ConnectionFactory() { HostName = rabbitHost };
                using (IConnection connection = factory.CreateConnection())
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    string message = JsonConvert.SerializeObject(item);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);

         
                    Thread.Sleep(2000);
                }
                return ServiceResponse<bool>.Success();
            }
            catch (Exception ex) 
            {
                return ServiceResponse<bool>.Failed(new ServiceError
                {
                    Message = ex.Message
                });
            }
            
        }
        
    }
}
