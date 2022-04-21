using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IRobotAlina.Data.Entities
{
    public class Tender
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("TenderMail")]
        [Required]
        public int TenderMailId { get; set; }

        [Required]
        [Column(TypeName = "varchar(128)")]
        public string ExternalId { get; set; }

        [Column(TypeName = "varchar(64)")]
        public string Number { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(2048)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "varchar(512)")]
        public string Url { get; set; }
        
        public string Description { get; set; }
                                
        [Required]
        public int Order { get; set; }

        [Required]
        public ETenderStatus Status { get; set; }   // Статус

        public Purchase Purchase { get; set; }      // Закупка

        public Customer Customer { get; set; }      // Заказчик

        public Result Result { get; set; }          // Результат
        
        public virtual TenderMail TenderMail { get; set; }

        public virtual ICollection<TenderFileAttachment> FileAttachments { get; set; }

        public Tender()
        {
            Status = ETenderStatus.Unknown;
        }
    }

    [Owned]
    public partial class Purchase
    {
        [Column(TypeName = "varchar(20)")]
        public string InitMinPrice { get; set; }        // НМЦ

        [Column(TypeName = "varchar(20)")]
        public string SecuringApp { get; set; }         // Обеспечение заявки

        [Column(TypeName = "varchar(20)")]
        public string SecuringContract { get; set; }    // Обеспечение контракта

        [Column(TypeName = "varchar(3)")]
        public string Currency { get; set; }            // Валюта закупки

        public DateTime? PublicationDate { get; set; }   // Дата публикации

        public DateTime? DeadlineAcceptApp { get; set; } // Окончание приема заявок

        public DateTime? ConductingSelection { get; set; }   // Проведение отбора

        [Column(TypeName = "varchar(128)")]
        public string SelectionStage { get; set; }      // Этап отбора

        [Column(TypeName = "varchar(128)")]
        public string TypeBidding { get; set; }         // Тип торгов

        [Column(TypeName = "varchar(256)")]
        public string SelectionMethod { get; set; }     // Способ отбора

        [Column(TypeName = "nvarchar(128)")]
        public string ETP { get; set; }                 // ЭТП

        [Column(TypeName = "varchar(50)")]
        public string Mark { get; set; }                // Метка

        [Column(TypeName = "nvarchar(2048)")]
        public string Comment { get; set; }             // Комментарий        
    }

    [Owned]
    public partial class Customer
    {
        [Column(TypeName = "nvarchar(1024)")]
        public string Region { get; set; }          // Регион

        [Column(TypeName = "nvarchar(1024)")]
        public string Name { get; set; }            // Название

        [Column(TypeName = "varchar(20)")]
        public string INN { get; set; }             // ИНН

        [Column(TypeName = "varchar(20)")]
        public string KPP { get; set; }             // КПП

        [Column(TypeName = "nvarchar(max)")]
        public string PlaceDelivery { get; set; }   // Место поставки        
    }

    [Owned]
    public partial class Result
    {        
        public DateTime? PublicationProtocol { get; set; }   // Публикация протокола

        [Column(TypeName = "nvarchar(1024)")]
        public string WinnerName { get; set; }              // Название победителя

        [Column(TypeName = "varchar(20)")]
        public string WinnerINN { get; set; }               // ИНН победителя

        [Column(TypeName = "varchar(512)")]
        public string WinnerOffer { get; set; }             // Предложение победителя

        [Column(TypeName = "varchar(20)")]
        public string PercenteDecline { get; set; }         // Процент снижения

        [Column(TypeName = "nvarchar(512)")]
        public string SupplierName { get; set; }            // Название поставщика

        [Column(TypeName = "varchar(20)")]
        public string SupplierINN { get; set; }             // ИНН поставщика

        [Column(TypeName = "varchar(20)")]
        public string ContractPrice { get; set; }           // Цена договора     
    }
}
