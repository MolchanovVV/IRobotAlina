namespace IRobotAlina.Data.Entities
{
    public interface IAttachment
    {
        public System.Guid Id { get; set; }           
        public int TenderId { get; set; }
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public ETenderFileAttachmentStatus? Status { get; set; }        
    }
}
