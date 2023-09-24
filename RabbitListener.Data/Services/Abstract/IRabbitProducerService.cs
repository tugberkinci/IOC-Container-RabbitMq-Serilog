using RabbitListener.Data.Repository.Abstract;
using RabbitListener.Data.Utilities.Response;
using RabbitListener.Dto.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitListener.Data.Services.Abstract
{
    public interface IRabbitProducerService : IBaseRepository<RabbitDto>
    {
        Task<ServiceResponse<bool>> ProduceAsync(RabbitDto item);
    }
}
