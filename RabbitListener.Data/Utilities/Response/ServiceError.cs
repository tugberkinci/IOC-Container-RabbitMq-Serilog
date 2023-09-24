using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitListener.Data.Utilities.Response
{
    /// <summary>
    /// Service layer error handler
    /// </summary>
    public class ServiceError
    {
        [Required]
        public string Message { get; set; } 
    }
}
