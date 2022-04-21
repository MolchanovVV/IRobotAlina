using IRobotAlina.Data.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace IRobotAlina.Web.Services.PrepareExcelFile
{
    public class ParseTenderAdditionalPartExcelData : IParseTenderAdditionalPartExcelData
    {
        public List<Tender> GetTenderAdditionalPart(string tenderAdditionalPartExctractedData)
        {
            List<Tender> result = new List<Tender>();

            if (string.IsNullOrWhiteSpace(tenderAdditionalPartExctractedData))
                return result;

            string[] tmp = tenderAdditionalPartExctractedData.Split(Environment.NewLine);

            if (tmp.Length == 0)
                return result;

            List<TenderAdditionalPartExcelCellInfo> TenderAdditionalPartExcelCellInfos = new List<TenderAdditionalPartExcelCellInfo>();
            for (int i = 0; i < tmp.Length - 1; i++)
            {
                TenderAdditionalPartExcelCellInfos.Add(JsonConvert.DeserializeObject<TenderAdditionalPartExcelCellInfo>(tmp[i]));
            }

            Tender newTenderAdditionalPart = null;
            bool resTryParseExact;
            string[] formats = { 
                "dd.MM.yyyy", 
                "dd.MM.yyyy h:mm", "dd.MM.yyyy h:mm:ss", "dd.MM.yyyy hh:mm", "dd.MM.yyyy hh:mm:ss", 
                "dd.MM.yyyy H:mm", "dd.MM.yyyy H:mm:ss", "dd.MM.yyyy HH:mm", "dd.MM.yyyy HH:mm:ss" };
            DateTime parsedDate;

            foreach (var group in TenderAdditionalPartExcelCellInfos.GroupBy(p => p.rowNumber))
            {
                newTenderAdditionalPart = new Tender();

                foreach (var item in group)
                {
                    string value = !string.IsNullOrWhiteSpace(item.value) ? item.value.Trim() : null;
                    
                    switch (item.sectionName.ToLowerInvariant())
                    {
                        case "закупка":
                            if (newTenderAdditionalPart.Purchase == null)
                                newTenderAdditionalPart.Purchase = new Purchase();

                            switch (item.name.ToLowerInvariant())
                            {
                                case "номер":                                    
                                    newTenderAdditionalPart.Number = value;
                                    break;

                                case "название":
                                    newTenderAdditionalPart.Name = value;                                    
                                    break;

                                case "нмц":
                                    newTenderAdditionalPart.Purchase.InitMinPrice = value;
                                    break;

                                case "обеспечение заявки":
                                    newTenderAdditionalPart.Purchase.SecuringApp = value;
                                    break;

                                case "обеспечение контракта":
                                    newTenderAdditionalPart.Purchase.SecuringContract = value;
                                    break;

                                case "валюта закупки":
                                    newTenderAdditionalPart.Purchase.Currency = value;
                                    break;

                                case "дата публикации":
                                    resTryParseExact = DateTime.TryParseExact(value, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);

                                    if (resTryParseExact)
                                        newTenderAdditionalPart.Purchase.PublicationDate = parsedDate;
                                    else
                                        newTenderAdditionalPart.Purchase.PublicationDate = null;
                                    break;

                                case "окончание приема заявок":
                                    resTryParseExact = DateTime.TryParseExact(value, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);

                                    if (resTryParseExact)
                                        newTenderAdditionalPart.Purchase.DeadlineAcceptApp = parsedDate;
                                    else
                                        newTenderAdditionalPart.Purchase.DeadlineAcceptApp = null;
                                    break;

                                case "проведение отбора":
                                    resTryParseExact = DateTime.TryParseExact(value, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);

                                    if (resTryParseExact)
                                        newTenderAdditionalPart.Purchase.ConductingSelection = parsedDate;
                                    else
                                        newTenderAdditionalPart.Purchase.ConductingSelection = null;
                                    break;

                                case "этап отбора":
                                    newTenderAdditionalPart.Purchase.SelectionStage = value;
                                    break;

                                case "тип торгов":
                                    newTenderAdditionalPart.Purchase.TypeBidding = value;
                                    break;

                                case "способ отбора":
                                    newTenderAdditionalPart.Purchase.SelectionMethod = value;
                                    break;

                                case "этп":
                                    newTenderAdditionalPart.Purchase.ETP = value;
                                    break;

                                case "метка":
                                    newTenderAdditionalPart.Purchase.Mark = value;
                                    break;

                                case "комментарий":
                                    newTenderAdditionalPart.Purchase.Comment = value;
                                    break;

                                default:
                                    break;
                            }
                            break; // case "закупка"..

                        case "заказчик":
                            if (newTenderAdditionalPart.Customer == null)
                                newTenderAdditionalPart.Customer = new Customer();

                            switch (item.name.ToLowerInvariant())
                            {
                                case "регион":
                                    newTenderAdditionalPart.Customer.Region = value;
                                    break;

                                case "название":
                                    newTenderAdditionalPart.Customer.Name = value;
                                    break;

                                case "инн":
                                    newTenderAdditionalPart.Customer.INN = value;
                                    break;

                                case "кпп":
                                    newTenderAdditionalPart.Customer.KPP = value;
                                    break;

                                case "место поставки":
                                    newTenderAdditionalPart.Customer.PlaceDelivery = value;
                                    break;

                                default:
                                    break;
                            }
                            break; // case "заказчик"..

                        case "результат":
                            if (newTenderAdditionalPart.Result == null)
                                newTenderAdditionalPart.Result = new Result();

                            switch (item.name.ToLowerInvariant())
                            {
                                case "публикация протокола":
                                    resTryParseExact = DateTime.TryParseExact(value, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);

                                    if (resTryParseExact)
                                        newTenderAdditionalPart.Result.PublicationProtocol = parsedDate;
                                    else
                                        newTenderAdditionalPart.Result.PublicationProtocol = null;
                                    break;

                                case "название победителя":
                                    newTenderAdditionalPart.Result.WinnerName = value;
                                    break;

                                case "инн победителя":
                                    newTenderAdditionalPart.Result.WinnerINN = value;
                                    break;

                                case "предложение победителя":
                                    newTenderAdditionalPart.Result.WinnerOffer = value;
                                    break;

                                case "процент снижения":
                                    newTenderAdditionalPart.Result.PercenteDecline = value;
                                    break;

                                case "название поставщика":
                                    newTenderAdditionalPart.Result.SupplierName = value;
                                    break;

                                case "инн поставщика":
                                    newTenderAdditionalPart.Result.SupplierINN = value;
                                    break;

                                case "цена договора":
                                    newTenderAdditionalPart.Result.ContractPrice = value;
                                    break;

                                default:
                                    break;
                            }
                            break; // case "результат"..

                        default:
                            break;
                    }

                    if (item == group.Last() && newTenderAdditionalPart != null)
                    {
                        result.Add(newTenderAdditionalPart);
                    }
                }
            }

            return result;
        }
    }
}
