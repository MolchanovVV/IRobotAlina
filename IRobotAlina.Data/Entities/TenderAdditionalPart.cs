using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IRobotAlina.Data.Entities
{
    public partial class TenderAdditionalPart
    {
        [Key]
        [ForeignKey("Tender")]
        public int Id { get; set; }
                
        public string ExternalId { get; set; }      // Номер
        
        public Purchase Purchase { get; set; }      // Закупка

        public Customer Customer { get; set; }      // Заказчик

        public Result Result { get; set; }          // Результат

        public ETenderStatus Status { get; set; }   // Статус
        
        public virtual Tender Tender { get; set; }        
    }    
}
