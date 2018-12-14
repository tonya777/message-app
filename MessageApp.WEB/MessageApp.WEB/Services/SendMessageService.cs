using MessageApp.WEB.Models;

namespace MessageApp.WEB.Services
{
    public static class SendMessageService
    {
        public static void SendMessage(UserModel recipient, MessageModel message)
        {
            message.IsSent = recipient.Id % 2 == 0;
        }
    }
}
