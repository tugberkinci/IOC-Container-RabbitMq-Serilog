using Newtonsoft.Json;

namespace RabbitListener.Producer.Response
{

    /// <summary>
    /// Generic api response model
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class GenericResponse
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public GenericResponse()
        {
            this.Details = new Details();
        }

        private static GenericResponse CreateInstance() => new GenericResponse();
       
        public static GenericResponse Success()
        {
            var model = GenericResponse.CreateInstance();
            return model;
        }

        public static GenericResponse Failed(
            string errorCode,
            string errorMessage
            )
        {
            var model = GenericResponse.CreateInstance();
            model.Details.ErrorCode = errorCode;
            model.Details.Message = errorMessage;

            return model;
        }

        public Details Details { get; set; }
       
    }

    public class Details
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public bool Success => String.IsNullOrEmpty(Message) ? true : false;
    }

   
}
