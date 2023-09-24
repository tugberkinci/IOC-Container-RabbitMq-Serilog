using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitListener.Data.Services.Concrete
{
    public static class RequestHandlerService
    {
        /// <summary>
        /// Async head request handler
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<int> HeadAsync(HttpClient httpClient, string url)
        {

            using (HttpRequestMessage request = new HttpRequestMessage())
            {
                try
                {
                    request.Method = HttpMethod.Head;
                    request.RequestUri = new Uri(url);

                    using HttpResponseMessage response = await httpClient.SendAsync(request);
                    var statusCode = (int)response.StatusCode;
                    return statusCode;
                }
                catch (HttpRequestException ex)
                {
                    //server error
                    return 500;
                }
                catch (Exception ex)
                {
                    //general error
                    return 0;
                    //throw;
                }
            }


        }
    }
}
