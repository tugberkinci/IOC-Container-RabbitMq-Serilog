using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitListener.Helper.Extension
{
    public static class ValidUrlExtension
    {
        /// <summary>
        /// Validates a URL.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static  bool ValidateUrl(this string url)
        {
            Uri validatedUri;

            if (Uri.TryCreate(url, UriKind.Absolute, out  validatedUri)) 
            {
                //If true: validatedUri contains a valid Uri. Check for the scheme in addition.
                return (validatedUri.Scheme == Uri.UriSchemeHttp || validatedUri.Scheme == Uri.UriSchemeHttps);
            }
            return false;
        }
    }
}
