using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace IRobotAlina.Web.Services.Download
{
    public class DownloadFileClient
    {
        private readonly ILogger<DownloadFileClient> logger;

        public DownloadFileClient(ILogger<DownloadFileClient> logger)
        {
            this.logger = logger;
        }

        public Task<byte[]> DownloadFile(string url, IEnumerable<Cookie> cookies)
        {
            try
            {
                var webRequest = (HttpWebRequest)WebRequest.Create(url);

                /* 
                //тоже нет
                IWebProxy proxy = WebRequest.GetSystemWebProxy();
                proxy.Credentials = CredentialCache.DefaultCredentials;
                webRequest.Proxy = proxy;
                
                // на обоих вариантах в лога ужасс типа
                //<p>Your cache administrator is <a href="mailto:root?subject=CacheErrorInfo%20-%20ERR_ACCESS_DENIED&amp;body=CacheHost%3A%20squid-all.mkm.lan%0D%0AErrPage%3A%20ERR_ACCESS_DENIED%0D%0AErr%3A%20%5Bnone%5D%0D%0ATimeStamp%3A%20Thu,%2002%20Dec%202021%2013%3A36%3A03%20GMT%0D%0A%0D%0AClientIP%3A%20172.31.0.66%0D%0A%0D%0AHTTP%20Request%3A%0D%0ACONNECT%20%2F%20HTTP%2F1.1%0AProxy-Authorization%3A%20Negotiate%20YIIGqAYGKwYBBQUCoIIGnDCCBpigMDAuBgkqhkiC9xIBAgIGCSqGSIb3EgECAgYKKwYBBAGCNwICHgYKKwYBBAGCNwICCqKCBmIEggZeYIIGWgYJKoZIhvcSAQICAQBuggZJMIIGRaADAgEFoQMCAQ6iBwMFACAAAACjggSGYYIEgjCCBH6gAwIBBaEJGwdNS00uTEFOoiQwIqADAgECoRswGRsESFRUUBsRc3F1aWQtYWxsLm1rbS5sYW6jggREMIIEQKADAgEXoQMCARGiggQyBIIELux0wxCTkoIcb2idfFwdyNP57RHe5osj238D7V0fVvIwaqqZaxGtjY%2FAa7x3Fyl5whiaoWqgjF3J9KrLZWVN8P22dm+MlvvKNYxJ1Afu177uxGR3ByNChyZCmSoekN3M0SbS7VtHEaQV5igm6Ta4z6fKOqX5j++PyjNRwodGDvNG0ceBOUUZdDslCyLidkvopBnU9EysMp9LbwqI1pm+oNlxa+kZOSfdGUf27GEnPQlR2KyaOinBBh9HSFzxOG62jZfz5sxT71KgVZI0UDWzoSYle79I9mGhbGYNSX6Prx+DZpsj4sD4Dj9gVPqzRcBaJAv1jXAYZuuurdTX5lZxZfT9Wy2b4Xtv%2FRNSPSFl7l98A28XxOGyskAhCjll2ErQhx3WUuGxiQvuBhzlaEl+h5TaTCVIKBOQE5LD6qjwLLOnDfc4zOw21JAuMyEZ0OcqM8qpshO+VxH0iOgOG3i4d+XzEVayGeJT1ELUPvoqQW0mfZYnw3DFaowF2F32BaxaFKlJ+mbilJiva9+M2YGBAK5UkpzC0bpVmmK%2Fyf808YfoKJ83N0sBT26d+LRCcS7+e5LlgvD8vLWxPNS1rFUAneDdWwN5Whrm%2FAxbveKk8CfFFyriudZdF616My9nS4Yx+m9QkEP7ChS74+heQN4yTONROGYZHhlwfbfAdLXkPjiTfPe5nAGrQ+5BTTqDLuz08vQGd3xHbIeS%2FYY6wAYhk66x3Topb84rRIIsf8UCi8+HO0NUanjZx%2F9D33522A3NNv7pZMZ0pRJFttzeLlzZdOhKHA3f21gUHlv2Hku6IMNEi9%2FkFP9zCoRYzheqyeTrViPWtsGexysDKFHXSuGrQQEbiHwGdmRC%2FvwTLUailfgdQ6y+Y0SxDW0DLpX7GynHxnKIGlq2ZVY8+pQPuMu8xU9KIOdXa7LynzpvWWe796rzZ99GS3CR2ndcYRXGUQsPCzcjls+LApfu9BOqx+PlC+7T9ohtsUFbHTcvuIlkbY9eD7Y5BuYj4kMjui%2Fia70dSGZHIkyYFBaMGkl0TtttMrW8xse59qnAcapgPOJagLa%2FYttq+hutVf2tu+%2Fovlym7+Wp9hH8rxXxdUayhek6hr%2F7SRfsDkat4CRde9JddbOfueXyXvgUYq5QoZg70FQt6dOEKYmpWDGWeJDdf6NMoEAA0nDCUBn1kXXsLtt7PL0QNJRkNd+qD3xb+Z29YF1B%2FNDi%2Frtlq9M4hkrRPNTLsJQwdAfS6NCHObHjLT1LAEMSjFxaOhlzTLHzNqwugfqT9Xs0wf%2Fj8QCq3mdOtF%2FHQWDUMAWC0XTnLo3rbzwLBM7qP+3GclN4FtxlqtIgzx1Y3lRdOgB6SYuddk8klDSRMMUG5Ey18TFI7y%2FnT2dE1mqSCES3cZILBNQGt4ahKcGN21+9IMRQpA19K5xRaqIipIIBpDCCAaCgAwIBF6KCAZcEggGTc63EijuI5bcpLdumD9yeJUpJEn0%2FkZ4zc2YaCIcW2i42+rTnQlpOF9LGf4g6uXOhMTjDT6NP2TLoocjvlnyjBevdovAVg2pJ2G2zaVwN1kkhqQhXZEFFmBmbz46MioYgrMWCRS58nZidgwNSW7%2F+3%2FSCQZNwauewQ2HjundAAqsWksVdwJLhYTqsXQYZuylOn2+rLZMbHS7CnH03Ya4pe+7YByNAYickN758EL8CXv%2FEj0cB%2FC3h7k2F%2F8VtcSTwQRAXxqDseMBQtcEbLAl6V4qltKyC5DElJ3sAuG7J7kFAXW2odpfPYcX3RrpXcv4XZSvUzICNT3wSDMysP3HPxebNMOP4l3y0ZlX9N6l8XKlbmlL5D5ZNKu5eCIgz%2FlxobjpatAKK9PT+v9lAd9hM7ljAx+T6OZZLFIKLcOl7cOhZZdXeiYzIXo5wwjQjwv46QthGaD6MQjp5z7C7vduswHiCQGAgTa9hKlw2+7Vah%2FKUTaXfIKjQ00N6gFNxufgRVGFiZQvSu1zIXjUakUhbOK9esA%3D%3D%0D%0AHost%3A%20www.tektorg.ru%3A443%0D%0A%0D%0A%0D%0A">root</a>.</p>                
                //

                // не взлетело
                webRequest.Proxy = WebRequest.DefaultWebProxy;
                webRequest.Credentials = CredentialCache.DefaultCredentials; ;
                webRequest.Proxy.Credentials = CredentialCache.DefaultCredentials;
                */


                webRequest.CookieContainer = new CookieContainer();
                foreach (var cookie in cookies)
                {
                    webRequest.CookieContainer.Add(cookie);
                }

                using var webResponse = (HttpWebResponse)webRequest.GetResponse();
                using var responseStream = webResponse.GetResponseStream();
                using var ms = new MemoryStream();
                responseStream.CopyTo(ms);

                return Task.FromResult(ms.ToArray());
            }
            catch (IOException ex)
            {
                // file is incomplete and throws error if we click it on website. We can't do anything with it
                if (!ex.Message.Contains("The response ended prematurely"))
                {
                    throw;
                }
            }
            catch (WebException ex)
            {
                var errResp = ex.Response;

                if (errResp != null)
                using (var respStream = errResp.GetResponseStream())
                {
                    var reader = new StreamReader(respStream);
                    logger.LogError($"Error getting file from the server({ex.Status} - {ex.Message}): \n{reader.ReadToEnd()}.");
                }
            }

            return Task.FromResult((byte[])null);
        }
    }
}
