using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitListener.Data.Utilities.Response
{
    /// <summary>
    /// Service layer response 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResponse<T>
    {
        private static readonly ServiceResponse<T> _success = new ServiceResponse<T> { Succeeded = true };
        private readonly List<ServiceError> _errors = new List<ServiceError>();

        public IEnumerable<ServiceError> Errors => _errors;
        public bool Succeeded { get; protected set; }
        public T? Data { get; set; }

        public static ServiceResponse<T> Failed(params ServiceError[] errors)
        {
            var result = new ServiceResponse<T> { Succeeded = false };
            if (errors != null)
            {
                result._errors.AddRange(errors);
            }
            return result;

        }

        public static ServiceResponse<T> Success(T Data)
        {
            var result = _success;
            result.Data = Data;
            return result;
        }

        public static ServiceResponse<T> Success()
        {
            return _success;
        }

        public override string ToString()
        {
            return Succeeded ?
                   "Succeeded" :
                   string.Format(CultureInfo.InvariantCulture, "{0} : {1}", "Failed", string.Join(",", Errors.Select(x => x.Message).ToList()));
        }

    }
}
