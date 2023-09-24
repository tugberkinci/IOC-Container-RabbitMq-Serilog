using RabbitListener.Dto.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitListener.Data.Repository.Abstract
{
    //Base service interface
    public interface IBaseRepository<TEntity> where TEntity : BaseDto
    {

    }
}
