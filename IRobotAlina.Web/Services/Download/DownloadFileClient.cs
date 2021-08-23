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
