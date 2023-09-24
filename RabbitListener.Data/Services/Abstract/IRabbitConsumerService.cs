using RabbitListener.Data.Repository.Abstract;
using RabbitListener.Dto.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitListener.Data.Services.Abstract
{
    public interface IRabbitConsumerService : IBaseRepository<RabbitDto>
    {
        void Consume(string serviceName);
    }
}
