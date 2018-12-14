using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessageApp.WEB.Models
{
    public class MessageModel
    {
        public int Id { get; set; }
        public string MessageId { get; set; }
        public string Subject { get; set; }
        public string MessageBody { get; set; }
        public bool IsSent { get; set; }
        public List<UserModel> Recipients { get; set; }
    }
}
