namespace MessageApp.WEB.DAL.DTO
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public string MessageId { get; set; }
        public string Subject { get; set; }
        public string MessageBody { get; set; }
        public bool IsSent { get; set; }
    }
}
