using MessageApp.WEB.DAL.DTO;
using MessageApp.WEB.Models;
using System.Collections.Generic;

namespace MessageApp.WEB.DAL
{
    public static class MessageService
    {
        public static void SaveMessage(MessageModel message, ApiContext context)
        {
            MessageDTO messageDTO = new MessageDTO()
            {
                Id = message.Id,
                IsSent = message.IsSent,
                MessageBody = message.MessageBody,
                MessageId = message.MessageId,
                Subject = message.Subject
            };

            context.Messages.Add(messageDTO);
            context.SaveChanges();

            SaveMessageToRecipientns(message, context);
        }

        private static void SaveMessageToRecipientns(MessageModel message, ApiContext _context)
        {
            List<RecipientsToMessages> recs = new List<RecipientsToMessages>();
            foreach (var rec in message.Recipients)
            {
                recs.Add(new RecipientsToMessages()
                {
                    MessageId = message.Id,
                    RecipientId = rec.Id
                });
            }

            _context.RecipientsToMessages.AddRange(recs);
            _context.SaveChanges();
        }

    }
}
