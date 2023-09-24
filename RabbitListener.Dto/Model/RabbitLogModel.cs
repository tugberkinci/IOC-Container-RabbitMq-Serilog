using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitListener.Dto.Model
{
    /// <summary>
    /// Log base model
    /// </summary>
    public  class RabbitLogModel
    {
        public static RabbitLogModel Instance(string serviceName,string url, int statusCode) 
            => new RabbitLogModel { ServiceName = serviceName, Url = url, StatusCode = statusCode };

        [Required]
        [Description("Rabbit service name")]
        public string ServiceName { get; protected set; }

        [Description("Consumed url")]
        public string Url { get; protected set; }

        [Description("Url request result")]
        public int StatusCode { get; protected set; }
    }
}
