using EAGetMail;
using IRobotAlina.Web.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IRobotAlina.Web.Services.Mails
{
    public class EWSMailProvider : IDisposable
    {
        MailServer oServer;
        MailClient oClient;
        bool isInitialized = false;

        Imap4Folder processedFolder;
        private readonly IEWSConfigurationProvider configurationProvider;
        private readonly IMailFilteringConfigurationProvider filteringConfigurationProvider;

        public EWSMailProvider(IEWSConfigurationProvider configurationProvider,
            IMailFilteringConfigurationProvider filteringConfigurationProvider)
        {
            this.configurationProvider = configurationProvider;
            this.filteringConfigurationProvider = filteringConfigurationProvider;
        }

        public void Initialize()
        {
            var configuration = configurationProvider.GetEWSConfiguration();

            oServer = new MailServer(configuration.IP,
                configuration.UserIdentity,
                configuration.Password,
                ServerProtocol.ExchangeEWS
            )
            {
                SSLConnection = true
            };

            oClient = new MailClient("EG-C1508812802-00584-3U5F4A3EADC3AAAE-U9TA61E86AF8DF79"); // ключ EAGetMail, честно купленный
            oClient.Connect(oServer);

            var folders = oClient.GetFolders();

            processedFolder = folders.FirstOrDefault(x => x.Name == "Processed");
            if (processedFolder == null)
            {
                processedFolder = oClient.CreateFolder(null, "Processed");
            }

            isInitialized = true;
        }

        public IEnumerable<EWSMail> GetEmails()
        {
            if (!isInitialized)
                Initialize();

            var emailsToInclude = filteringConfigurationProvider.GetSenderEmailsToInclude();

            var infos = oClient.GetMailInfos();
            var mails = new List<EWSMail>();
            for (int i = 0; i < infos.Length; i++)
            {
                var info = infos[i];
                Mail oMail = oClient.GetMail(info);

                if (emailsToInclude.Count() == 0
                    || emailsToInclude.Contains(oMail.From.Address))
                {
                    mails.Add(new EWSMail()
                    {                        
                        Mail = oMail,
                        UIDL = info.UIDL,
                    });
                }
            }

            return mails;
        }

        public void MarkAsProcessed(string uidl)
        {
            if (!isInitialized)
                Initialize();

            var infos = oClient.GetMailInfos();
            var mailInfo = infos.FirstOrDefault(x => x.UIDL == uidl);

            oClient.Move(mailInfo, processedFolder);
        }

        public void MarkAsRead(string uidl)
        {
            if (!isInitialized)
                Initialize();

            var infos = oClient.GetMailInfos();
            var mailInfo = infos.FirstOrDefault(x => x.UIDL == uidl);

            oClient.MarkAsRead(mailInfo, true);
        }

        public void Dispose()
        {
            oClient?.Quit();
            oClient?.Close();
        }
    }

    public class EWSMail
    {
        public string UIDL { get; set; }

        public Mail Mail { get; set; }        
    }
}
