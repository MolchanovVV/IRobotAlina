using IRobotAlina.Web.Services.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace IRobotAlina.Web.Services.Scraper
{
    public class SeleniumZakupkiKonturScraper : IZakupkiKonturScraper, IDisposable
    {
        private IWebDriver webDriver;
        private bool isInitialized = false;
        private const string baseUrl = "https://auth.kontur.ru/?back=http%3A%2F%2Fzakupki.kontur.ru%2FGrid&customize=zakupki&tabs=1%2C1%2C0%2C0";
        private readonly IZakupkiKonturCredentialsProvider credentialsProvider;
        private readonly HttpClient httpClient;

        public SeleniumZakupkiKonturScraper(IZakupkiKonturCredentialsProvider credentialsProvider, IHttpClientFactory httpClientFactory)
        {
            this.credentialsProvider = credentialsProvider;
            httpClient = httpClientFactory.CreateClient(nameof(SeleniumZakupkiKonturScraper));
        }

        private async Task<HttpStatusCode> GetStatusCode()
        {
            var response = await httpClient.GetAsync(webDriver.Url);

            return response.StatusCode;
        }

        /// <summary>
        /// Navigates to auth page. 
        /// When authenticated flips isIntialized to true, so we can use same instance multiple times
        /// </summary>
        public void Initialize()
        {
            var credentials = credentialsProvider.GetCredentials();

            webDriver = new ChromeDriver
            {
                Url = baseUrl
            };

            // wait until login view is rendered by React
            var emailBy = By.XPath("//input[@name=\"login\"]");
            var passwordBy = By.XPath("//input[@type=\"password\"]");
            Wait(x => x.FindElements(emailBy).Count > 0);

            webDriver.FindElement(emailBy).SendKeys(credentials.Email);
            webDriver.FindElement(passwordBy).SendKeys(credentials.Password);

            var submitButtonBy = By.XPath("//button[@type=\"submit\"]");

            webDriver.FindElement(submitButtonBy).Click();

            Wait(x => x.Url == "https://zakupki.kontur.ru/Grid");

            isInitialized = true;
        }

        private void Wait(Func<IWebDriver, bool> until, int seconds = 10)
        {
            new WebDriverWait(webDriver, TimeSpan.FromSeconds(seconds)).Until(until);
        }

        public async Task<TenderDto> GetTenderInfo(string tenderUrl)
        {
            if (!isInitialized)
                Initialize();
            
            if (webDriver.Url != tenderUrl)
                webDriver.Url = tenderUrl;

            if (await GetStatusCode() != HttpStatusCode.OK)
                return null;

            var tenderNumberBy = By.XPath("//p[contains(@class, \"tender_title\")]");
            Wait(x => webDriver.FindElements(tenderNumberBy).Count > 0, 15);
            var tmpTenderNumber = webDriver.FindElement(tenderNumberBy)?.Text;

            string tenderNumber = null;
            int index = tmpTenderNumber.IndexOf("№");
            if (index >= 0)
            {
                tenderNumber = tmpTenderNumber.Substring(index, tmpTenderNumber.Length - index);

                tenderNumber = tenderNumber.Replace("№", "").Trim();
            }

            var tenderTitleBy = By.XPath("//div[@class=\"blockTitle blockTitle__main\"]/h1");                                                       
            Wait(x => webDriver.FindElements(tenderTitleBy).Count > 0, 15);
            var tenderTitle = webDriver.FindElement(tenderTitleBy)?.Text;

            var tenderDescription = new StringBuilder();

            CloseSurveyPopupIfOpen();
                        
            var purchaseInfoWraps = By.XPath("//div[contains(@class, \"purchaseInfoWrap\")]").FindElements(webDriver);            
            for (var i = 0; i < purchaseInfoWraps.Count; i++)
            {
                try
                {
                    tenderDescription.AppendLine(purchaseInfoWraps[i].Text);                    
                }
                catch { }
            }
                       
            return new TenderDto()
            {                
                Number = tenderNumber,
                Name = tenderTitle,
                Description = tenderDescription.ToString(),
                Documents = GetDocumentUrls(tenderUrl)
            };
        }

        /// <summary>
        /// Navigates to tender url page and extracts links to files
        /// </summary>
        /// <param name="tenderUrl"></param>
        /// <returns></returns>
        public IEnumerable<TenderDocumentDto> GetDocumentUrls(string tenderUrl)
        {
            if (!isInitialized)
                Initialize();

            if (webDriver.Url != tenderUrl)
                webDriver.Url = tenderUrl;

            CloseSurveyPopupIfOpen();
            var fileAttachmentsBy = By.XPath("//div[@id=\"modal-showDocuments\"]//a[contains(@class, \"purchase-attachment__downloadLink\")]");
            var elements = webDriver.FindElements(fileAttachmentsBy);

            CloseSurveyPopupIfOpen();
            if (elements.Count == 0)
            {
                fileAttachmentsBy = By.XPath("//a[contains(@class, \"purchase-attachment__downloadLink\")]");
                elements = webDriver.FindElements(fileAttachmentsBy);
            }
            else
            {
                CloseSurveyPopupIfOpen();
                var showModalBy = By.XPath("//button[@id=\"showDocuments\"]");

                var modalButton = webDriver.FindElement(showModalBy);
                modalButton.Click();

                Wait(x => x.FindElements(By.XPath("//div[@class=\"modal in\"]")).Count > 0);
            }

            CloseSurveyPopupIfOpen();

            foreach (var linkElement in elements)
            {
                var link = linkElement.GetAttribute("href");
                yield return new TenderDocumentDto()
                {
                    Url = link,
                    FileName = linkElement.Text
                };
            }
        }

        private void CloseSurveyPopupIfOpen()
        {
            try
            {
                var buttons = webDriver.FindElements(By.XPath("//button[@class=\"survey-popup_close\"]"));
                if (buttons.Count > 0)
                {
                    foreach (var button in buttons)
                    {
                        button.Click();
                    }
                }
            }
            catch { }
        }

        public IEnumerable<System.Net.Cookie> GetCookies()
        {
            if (!isInitialized)
                Initialize();

            var cookies = webDriver.Manage().Cookies;

            foreach (var cookie in cookies.AllCookies)
            {
                yield return new System.Net.Cookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain);
            }
        }

        //public async Task<bool> DownloadFileTendersAdditionalPart(string url)        
        public void DownloadFileTendersAdditionalPart(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return;
            
            if (!isInitialized)
                Initialize();

            url = url.Replace("&amp;", "&");            
            
            if (webDriver.Url != url)
                webDriver.Url = url;
            
            //if (await GetStatusCode() != HttpStatusCode.OK)
            //    return false;
            
            Thread.Sleep(2000);

            //return true;
        }

        public void Dispose()
        {
            webDriver?.Dispose();
        }
    }
}
