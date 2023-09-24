using RabbitListener.Dto.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitListener.Dto.Concrete
{
    /// <summary>
    /// Rabbitmq queue dto
    /// </summary>
    public class RabbitDto : BaseDto , IBaseDto
    {
        [Required]
        [Description("Url")]
        public string Url { get; set; }
    }
}
