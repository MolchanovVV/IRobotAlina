﻿using IRobotAlina.Web.Models;
using IRobotAlina.Web.Services.Mails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IRobotAlina.Web.Services.TenderLinkProvider
{
    public class ZakupkiKonturDummyEmailTenderMailProvider : ITenderMailProvider, IZakupkiKonturTenderMailProvider
    {
        private const string _htmlFile = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"><div dir=\"ltr\"><br><br><div class=\"gmail_quote\"><div dir=\"ltr\" class=\"gmail_attr\">---------- Forwarded message ---------<br>From: <strong class=\"gmail_sendername\" dir=\"auto\">Yegor Androsov</strong> <span dir=\"auto\">&lt;<a href=\"mailto:yegor.androsov @gmail.com\" target=\"_blank\">yegor.androsov@gmail.com</a>&gt;</span><br>Date: Tue, Oct 27, 2020 at 1:58 PM<br>Subject: Fwd: Новые закупки по вашим шаблонам<br>To:  &lt;<a href=\"mailto:mkmtest @outlook.com\" target=\"_blank\">mkmtest@outlook.com</a>&gt;<br></div><br><br><div dir=\"ltr\"><br><br><div class=\"gmail_quote\"><div dir=\"ltr\" class=\"gmail_attr\">---------- Forwarded message ---------<br>From: <strong class=\"gmail_sendername\" dir=\"auto\">Молчанов Вячеслав</strong> <span dir=\"auto\">&lt;<a href=\"mailto:tenarrows @yandex.ru\" target=\"_blank\">tenarrows@yandex.ru</a>&gt;</span><br>Date: Sun, Oct 25, 2020 at 10:24 AM<br>Subject: Fwd: Новые закупки по вашим шаблонам<br>To: Егор Андросов &lt;<a href=\"mailto:yegor.androsov @gmail.com\" target=\"_blank\">yegor.androsov@gmail.com</a>&gt;<br></div><br><br><div>&nbsp;</div><div><div>почта <a href=\"mailto:mkmtest @outlook.com\" rel=\"noopener noreferrer\" target=\"_blank\">mkmtest@outlook.com</a></div><div>пароль TestRun01</div><div>&nbsp;</div><div>-------- Пересылаемое сообщение --------</div><div>25.10.2020, 10:29, &quot;Салобаева Екатерина Викторовна&quot; &lt;<a href=\"mailto:salobaevaev @mkm.ru\" rel=\"noopener noreferrer\" target=\"_blank\">salobaevaev@mkm.ru</a>&gt;:</div><div><span style=\"color:#1f497d;font-family:'calibri',sans-serif;font-size:11pt\">&nbsp;</span></div><div><div bgcolor=\"#F4F3F4\" lang=\"RU\"><div><div><div style=\"border-style:solid none none none;border-top-color:#e1e1e1;border-width:1pt medium medium medium;padding:3pt 0cm 0cm 0cm\"><p><strong><span style=\"font-family:'calibri',sans-serif;font-size:11pt\">From:</span></strong><span style=\"font-family:'calibri',sans-serif;font-size:11pt\"> Контур.Закупки [<a href=\"mailto:subscribe@zakupki.kontur.ru\" rel=\"noopener noreferrer\" target=\"_blank\">mailto:subscribe@zakupki.kontur.ru</a>]<br><strong>Sent:</strong> Sunday, October 25, 2020 7:44 AM<br><strong>To:</strong> Салобаева Екатерина Викторовна &lt;<a href=\"mailto:SalobaevaEV@mkm.ru\" rel=\"noopener noreferrer\" target=\"_blank\">SalobaevaEV@mkm.ru</a>&gt;<br><strong>Subject:</strong> Новые закупки по вашим шаблонам</span></p></div></div><p>&nbsp;</p><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"background:#f4f3f4;border-collapse:collapse;width:100%\"><tbody><tr><td style=\"padding:0cm\"><p><span style=\"color:#222222;font-family:'segoe ui',sans-serif\">&nbsp;</span></p></td><td valign=\"top\" width=\"700\" style=\"padding:0cm;width:525pt\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"0\" style=\"border-collapse:collapse;border-spacing:0;float:none;width:525pt\"><tbody><tr style=\"height:21.75pt\"><td style=\"height:21.75pt;padding:28.5pt 15pt 15pt 15pt\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"0\" style=\"border-collapse:collapse;border-spacing:0;width:157.5pt\"><tbody><tr><td valign=\"top\" style=\"padding:0cm\"><p><span style=\"font-family:'segoe ui',sans-serif\"><a href=\"https://zakupki.kontur.ru/\" rel=\"noopener noreferrer\" title=\"Контур.Закупки\" target=\"_blank\"><span style=\"font-size:22pt\"><img alt=\"контур.закупки\" border=\"0\" src=\"http://zakupki.kontur.ru/content/images/email-logo.png\" width=\"210\"></span></a></span></p></td></tr></tbody></table></td></tr><tr><td style=\"background:white;border-left-color:#d9d9d9;border-right-color:#d9d9d9;border-style:solid solid none solid;border-top-color:#1589ca;border-width:2.25pt 1pt medium 1pt;padding:18.75pt 15pt 20.25pt 15pt\"><h1 style=\"margin:0cm 0cm 0.0001pt 0cm;word-wrap:normal\"><span style=\"color:#222222;font-family:'segoe ui',sans-serif;font-size:16.5pt;font-weight:normal\">Уведомления по закупкам за&nbsp;24 окт 14:00 – 25 окт 07:00 </span><span style=\"color:#b3b3b3;font-family:'segoe ui',sans-serif;font-size:16.5pt;font-weight:normal;text-transform:uppercase\">мск</span><span style=\"color:#222222;font-family:'segoe ui',sans-serif;font-size:16.5pt;font-weight:normal\"> </span></h1><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"border-collapse:collapse;border-spacing:0;width:100%\"><tbody><tr><td valign=\"top\" style=\"padding:0cm\"><p style=\"line-height:18.75pt\"><span style=\"font-family:'segoe ui',sans-serif;font-size:19pt\">&nbsp; </span></p></td></tr></tbody></table><p><span style=\"font-family:'segoe ui',sans-serif\">&nbsp;</span></p><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"0\" style=\"border-collapse:collapse;border-spacing:0;width:0cm\"><tbody><tr style=\"height:20.25pt\"><td style=\"height:20.25pt;padding:0cm 15pt 7.5pt 0cm\"><p><span style=\"color:#222222;font-family:'segoe ui',sans-serif\"><a href=\"https://zakupki.kontur.ru/NewPurchases?from=2020-10-24T14%3A00%3A00%2B03%3A00&amp;to=2020-10-25T07%3A00%3A00%2B03%3A00&amp;utm_campaign=subscription&amp;utm_content=templates-link&amp;utm_medium=email&amp;utm_source=zakupki\" rel=\"noopener noreferrer\" target=\"_blank\"><strong>4 закупки </strong></a>по шаблонам </span></p></td><td valign=\"top\" style=\"height:20.25pt;padding:0cm 0cm 7.5pt 0cm\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"0\" style=\"border-collapse:collapse;border-spacing:0;width:0cm\"><tbody><tr style=\"height:20.25pt\"><td width=\"18\" style=\"height:20.25pt;padding:0cm 3.75pt 0cm 0cm;width:9.75pt\"><p><span style=\"color:#222222;font-family:'segoe ui',sans-serif;font-size:10pt\"><img alt=\"↧\" border=\"0\" height=\"13\" src=\"https://zakupki.kontur.ru/content/images/email-export-icon.png\" width=\"13\"></span></p></td><td style=\"height:20.25pt;padding:0cm\"><p><span style=\"color:#222222;font-family:'segoe ui',sans-serif\"><a href=\"https://zakupki.kontur.ru/NewPurchases/DownloadExcel?from=2020-10-24T14%3A00%3A00%2B03%3A00&amp;to=2020-10-25T07%3A00%3A00%2B03%3A00&amp;utm_campaign=subscription&amp;utm_content=templates-link&amp;utm_medium=excel&amp;utm_source=zakupki\" rel=\"noopener noreferrer\" target=\"_blank\">Выгрузить в Excel </a></span></p></td></tr></tbody></table></td></tr></tbody></table><p><span style=\"font-family:'segoe ui',sans-serif\">&nbsp;</span></p><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"border-collapse:collapse;border-spacing:0;width:100%\"><tbody><tr><td style=\"border-bottom-color:#e4e4e4;border-style:none none solid none;border-width:medium medium 1pt medium;max-width:580px;padding:0cm\"><p style=\"line-height:12.75pt\"><span style=\"font-family:'segoe ui',sans-serif;font-size:13pt\">&nbsp;</span></p></td></tr></tbody></table><div style=\"margin-top:17.25pt\"><p><span style=\"color:#222222;font-family:'segoe ui',sans-serif\">шаблон для тестирования (4) </span></p></div><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"width:100%\"><tbody><tr><td valign=\"top\" style=\"padding:12.75pt 0cm 0cm 0cm;word-wrap:normal\"><p><span style=\"color:#222222;font-family:'segoe ui',sans-serif\">1. </span></p></td><td valign=\"top\" style=\"padding:12.75pt 0cm 0cm 13.5pt;word-wrap:normal\"><div style=\"margin-bottom:7.5pt\"><p><span style=\"color:#222222;font-family:'segoe ui',sans-serif\"><a href=\"https://zakupki.kontur.ru/2488428_4?utm_source=zakupki&amp;utm_campaign=subscription&amp;utm_medium=email&amp;utm_content=7722743350\" rel=\"noopener noreferrer\" target=\"_blank\">Приборы и комплектующие для учета энергоресурсов </a></span></p></div><div style=\"margin-bottom:7.5pt\"><p><span style=\"color:#222222;font-family:'segoe ui',sans-serif\">АО «Тзрк» </span></p></div><div style=\"margin-bottom:7.5pt\"><p><span style=\"color:#222222;font-family:'segoe ui',sans-serif\">Подача заявок до 30 октября 2020 г. </span></p></div><div><p><span style=\"color:#999999;font-family:'segoe ui',sans-serif\">Коммерческие, №&nbsp;­2488428 </span></p></div></td></tr><tr><td valign=\"top\" style=\"padding:12.75pt 0cm 0cm 0cm;word-wrap:normal\"><p><span style=\"color:#222222;font-family:'segoe ui',sans-serif\">2. </span></p></td><td valign=\"top\" style=\"padding:12.75pt 0cm 0cm 13.5pt;word-wrap:normal\"><div style=\"margin-bottom:7.5pt\"><p><span style=\"color:#222222;font-family:'segoe ui',sans-serif\"><a href=\"https://zakupki.kontur.ru/GP058586?utm_source=zakupki&amp;utm_campaign=subscription&amp;utm_medium=email&amp;utm_content=7722743350\" rel=\"noopener noreferrer\" target=\"_blank\">Закупка взрывозащищенного МТР </a></span></p></div><div style=\"margin-bottom:7.5pt\"><p><span style=\"color:#222222;font-family:'segoe ui',sans-serif\">ООО «Газэнергострой» </span></p></div><div style=\"margin-bottom:7.5pt\"><p><span style=\"color:#222222;font-family:'segoe ui',sans-serif\">Подача заявок до 30 октября 2020 г. </span></p></div><div><p><span style=\"color:#999999;font-family:'segoe ui',sans-serif\">Коммерческие, №&nbsp;­ГП058586 </span></p></div></td></tr><tr><td valign=\"top\" style=\"padding:12.75pt 0cm 0cm 0cm;word-wrap:normal\"><p><span style=\"color:#222222;font-family:'segoe ui',sans-serif\">3. </span></p></td><td valign=\"top\" style=\"padding:12.75pt 0cm 0cm 13.5pt;word-wrap:normal\"><div style=\"margin-bottom:7.5pt\"><p><span style=\"color:#222222;font-family:'segoe ui',sans-serif\"><a href=\"https://zakupki.kontur.ru/IS18314792?utm_source=zakupki&amp;utm_campaign=subscription&amp;utm_medium=email&amp;utm_content=7722743350\" rel=\"noopener noreferrer\" target=\"_blank\">Кабельная продукция </a></span></p></div><div style=\"margin-bottom:7.5pt\"><p><span style=\"color:#222222;font-family:'segoe ui',sans-serif\">АО «Роспан Интернешнл» </span></p></div><div style=\"margin-bottom:7.5pt\"><p><span style=\"color:#222222;font-family:'segoe ui',sans-serif\">Подача заявок до 2 ноября 2020 г. </span></p></div><div><p><span style=\"color:#999999;font-family:'segoe ui',sans-serif\">Коммерческие, №&nbsp;­РН01009952 </span></p></div></td></tr><tr><td valign=\"top\" style=\"padding:12.75pt 0cm 0cm 0cm;word-wrap:normal\"><p><span style=\"color:#222222;font-family:'segoe ui',sans-serif\">4. </span></p></td><td valign=\"top\" style=\"padding:12.75pt 0cm 0cm 13.5pt;word-wrap:normal\"><div style=\"margin-bottom:7.5pt\"><p><span style=\"color:#222222;font-family:'segoe ui',sans-serif\"><a href=\"https://zakupki.kontur.ru/0321100017620000243?utm_source=zakupki&amp;utm_campaign=subscription&amp;utm_medium=email&amp;utm_content=7722743350\" rel=\"noopener noreferrer\" target=\"_blank\">Прокладка кабельных линий сетей электроснабжения в Санатории им. С.М. Кирова ФФГБУ СКФНКЦ ФМБА России в г. Пятигорске </a></span></p></div><div style=\"margin-bottom:7.5pt\"><p><span style=\"color:#222222;font-family:'segoe ui',sans-serif\">Федеральное ГБУ «Северо-Кавказский Федеральный Научно-Клинический Центр Федерального Медико-Биологического Агентства» </span></p></div><div style=\"margin-bottom:7.5pt\"><p><span style=\"color:#222222;font-family:'segoe ui',sans-serif;font-size:13.5pt\">4 541 129,00 </span><span style=\"color:#222222;font-family:'segoe ui',sans-serif\">руб. Подача заявок до 2 ноября 2020 г. </span></p></div><div><p><span style=\"color:#999999;font-family:'segoe ui',sans-serif\">44-ФЗ, №&nbsp;­0321100017620000243 </span></p></div></td></tr></tbody></table></td></tr><tr><td style=\"background:#fafafa;border-color:#d9d9d9;border-style:none solid solid solid;border-width:medium 1pt 1pt 1pt;padding:11.25pt 15pt 13.5pt 15pt\"><p><span style=\"color:#222222;font-family:'segoe ui',sans-serif;font-size:10.5pt\">По вопросам звоните <a href=\"tel:88005000633\" rel=\"noopener noreferrer\" target=\"_blank\"><span style=\"color:#222222\">8&nbsp;800&nbsp;500&nbsp;06&nbsp;33 </span></a>или&nbsp;пишите на&nbsp; <a href=\"mailto:zakupki@kontur.ru\" rel=\"noopener noreferrer\" target=\"_blank\">zakupki@kontur.ru </a> </span></p></td></tr><tr><td style=\"background:#f4f3f4;padding:13.5pt 15pt 15pt 15pt\"><div><p><span style=\"color:gray;font-family:'segoe ui',sans-serif;font-size:10.5pt\">Вы&nbsp;получили это письмо потому, что подписаны на&nbsp;уведомления о&nbsp;закупках по&nbsp;шаблонам в&nbsp;сервисе <a href=\"https://zakupki.kontur.ru/\" rel=\"noopener noreferrer\" target=\"_blank\">Контур.Закупки</a>. </span></p></div><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"border-collapse:collapse;border-spacing:0;width:100%\"><tbody><tr><td valign=\"top\" style=\"padding:0cm\"><p style=\"line-height:15pt\"><span style=\"font-family:'segoe ui',sans-serif;font-size:15pt\">&nbsp; </span></p></td></tr></tbody></table><p><span style=\"color:gray;font-family:'segoe ui',sans-serif;font-size:10.5pt\">&nbsp;</span></p><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"0\" style=\"border-collapse:collapse;border-spacing:0;width:0cm\"><tbody><tr><td valign=\"top\" style=\"padding:0cm 0cm 15pt 0cm\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"border-collapse:collapse;border-spacing:0;width:100%\"><tbody><tr><td valign=\"top\" style=\"padding:0cm 11.25pt 0cm 0cm\"><p><span style=\"color:#222222;font-family:'segoe ui',sans-serif\"><a href=\"https://mail.kontur.ru/track/00000032/5f95027c1b41cf05e882cbf2/5f95027c1b41cf05e882cbf4/unsubscribe\" rel=\"noopener noreferrer\" target=\"_blank\"><span style=\"font-size:10.5pt\">Отписаться от&nbsp;уведомлений </span></a></span></p></td></tr></tbody></table></td><td style=\"padding:0cm 0cm 15pt 0cm\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"border-collapse:collapse;border-spacing:0;width:100%\"><tbody><tr><td valign=\"top\" style=\"padding:0cm\"><p><span style=\"color:#222222;font-family:'segoe ui',sans-serif\"><a href=\"https://zakupki.kontur.ru/SubscriptionSettings\" rel=\"noopener noreferrer\" target=\"_blank\"><span style=\"font-size:10.5pt\">Настроить уведомления по шаблонам </span></a></span></p></td></tr></tbody></table></td></tr></tbody></table><p><span style=\"color:gray;font-family:'segoe ui',sans-serif;font-size:10.5pt\">&nbsp;</span></p><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"0\" style=\"border-collapse:collapse;border-spacing:0;width:0cm\"><tbody><tr style=\"height:20.25pt\"><td style=\"height:20.25pt;padding:0cm;word-wrap:normal\"><p style=\"line-height:16.5pt\"><span style=\"color:gray;font-family:'segoe ui',sans-serif;font-size:10.5pt\"><img alt=\"СКБ Контур\" border=\"0\" height=\"22\" src=\"https://zakupki.kontur.ru/content/images/email-logo-skb.png\" width=\"121\"></span></p></td><td style=\"height:20.25pt;padding:0cm;word-wrap:normal\"><p><span style=\"color:gray;font-family:'segoe ui',sans-serif;font-size:10.5pt\">|</span></p></td><td style=\"height:20.25pt;padding:0cm 0cm 0cm 6.75pt;word-wrap:normal!important\"><p><span style=\"color:gray;font-family:'segoe ui',sans-serif;font-size:10.5pt\">Народной Воли,&nbsp;19а, Екатеринбург, Россия, 620017 </span></p></td></tr></tbody></table><div><p style=\"line-height:0%\"><span style=\"color:#f4f3f4;font-family:'segoe ui',sans-serif;font-size:1pt\">СКБ Контур |&nbsp; Народной Воли,&nbsp;19а, Екатеринбург, Россия, 620017 </span></p></div></td></tr></tbody></table></td><td style=\"padding:0cm\"><p><span style=\"color:#222222;font-family:'segoe ui',sans-serif\">&nbsp;</span></p></td></tr></tbody></table><p><img border=\"0\" height=\"1\" src=\"https://mail.kontur.ru/track/00000032/5f95027c1b41cf05e882cbf2/open\" width=\"1\"></p></div></div></div><div>&nbsp;</div><div>-------- Конец пересылаемого сообщения --------</div><div>&nbsp;</div><div>&nbsp;</div><div>---</div><div style=\"font-size:13.5px;white-space:nowrap\">С уважением,<br>Молчанов Вячеслав</div><div>&nbsp;</div></div><div>&nbsp;</div><div>-------- Конец пересылаемого сообщения --------</div><div>&nbsp;</div><div>&nbsp;</div><div>---</div><div style=\"font-size:13.5px;white-space:nowrap\">С уважением,<br>Молчанов Вячеслав</div><div>&nbsp;</div></div></div>";
        private readonly IZakupkiKonturMailParser mailParser;

        public ZakupkiKonturDummyEmailTenderMailProvider(IZakupkiKonturMailParser mailParser)
        {
            this.mailParser = mailParser;
        }

        public Task<List<TenderMailDto>> GetTenderMails()
        {            
            mailParser.GetLinks(_htmlFile);

            return Task.FromResult(new List<TenderMailDto>());
        }

        public void MarkAsCompleted(List<TenderMailDto> items)
        {
        }

        public void MarkAsCompleted(TenderMailDto item)
        {
        }
    }
}
