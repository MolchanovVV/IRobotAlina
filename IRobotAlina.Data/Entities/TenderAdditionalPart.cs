using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IRobotAlina.Data.Entities
{
    public class TenderAdditionalPart
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("Tender")]
        public string ExternalId { get; set; }      // Номер

        public Purchase Purchase { get; set; }      // Закупка

        public Customer Customer { get; set; }      // Заказчик

        public Result Result { get; set; }          // Результат

        public ETenderStatus Status { get; set; }   // Статус
        
        public virtual Tender Tender { get; set; }
    }

    [ComplexType]
    public class Purchase
    {
        public string Name { get; set; }                // Наименование

        public string InitMinPrice { get; set; }        // НМЦ

        public string SecuringApp { get; set; }         // Обеспечение заявки

        public string SecuringContract { get; set; }    // Обеспечение контракта

        [Column(TypeName = "char(3)")]
        public char[] Currency { get; set; }            // Валюта закупки

        public DateTime PublicationDate { get; set; }   // Дата публикации

        public DateTime DeadlineAcceptApp { get; set; } // Окончание приема заявок

        public DateTime ConductingSelection { get; set; }   // Проведение отбора

        public string SelectionStage { get; set; }      // Этап отбора

        public string TypeBidding { get; set; }         // Тип торгов

        public string SelectionMethod { get; set; }     // Способ отбора

        public string ETP { get; set; }                 // ЭТП

        public string Mark { get; set; }                // Метка

        public string Comment { get; set; }             // Комментарий
    }

    [ComplexType]
    public class Customer
    {
        public string Region { get; set; }          // Регион

        public string Name { get; set; }            // Название

        public string INN { get; set; }             // ИНН

        public string KPP { get; set; }             // КПП

        public string PlaceDelivery { get; set; }   // Место поставки
    }

    [ComplexType]
    public class Result
    {
        public DateTime PublicationProtocol { get; set; }   // Публикация протокола

        public string WinnerName { get; set; }              // Название победителя

        public string WinnerINN { get; set; }               // ИНН победителя

        public string WinnerOffer { get; set; }             // Предложение победителя

        public string PercenteDecline { get; set; }         // Процент снижения

        public string SupplierName { get; set; }            // Название поставщика

        public string SupplierINN { get; set; }             // ИНН поставщика

        public string ContractPrice { get; set; }           // Цена договора
    }
}
