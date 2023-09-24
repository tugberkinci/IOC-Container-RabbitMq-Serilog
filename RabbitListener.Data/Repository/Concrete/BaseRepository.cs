using RabbitListener.Data.Repository.Abstract;
using RabbitListener.Dto.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitListener.Data.Repository.Concrete
{
    /// <summary>
    /// Base service class
    /// </summary>
    /// <typeparam name="Entity"></typeparam>
    public class BaseRepository<Entity> : IBaseRepository<Entity> where Entity : BaseDto , new()
    {
        public BaseRepository() { }
    }
}
