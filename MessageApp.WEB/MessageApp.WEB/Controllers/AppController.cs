using System;
using System.Collections.Generic;
using System.Linq;
using MessageApp.WEB.DAL;
using MessageApp.WEB.Models;
using MessageApp.WEB.Services;
using Microsoft.AspNetCore.Mvc;

namespace MessageApp.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        public List<MessageModel> Messages { get; set; }

        private readonly ApiContext _context;

        public AppController(ApiContext context)
        {
            _context = context;
            Messages = new List<MessageModel>();
        }
        
        [HttpGet]
        public IEnumerable<UserModel> Get()
        {
            var users = _context.Users.Select(user => new UserModel
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname
            });

            return users.ToArray();
        }
        
        [HttpPost]
        public List<ResponceModel> Post([FromBody] MessageModel message)
        {
            var id = Guid.NewGuid().ToString();
            message.MessageId = id;

            var responses = new List<ResponceModel>();

            foreach (var recipient in message.Recipients)
            {
                SendMessageService.SendMessage(recipient, message);
                responses.Add(new ResponceModel
                {
                    MessageId = message.MessageId,
                    IsSent = message.IsSent,
                    Recipient = String.Format("{0} {1}", recipient.Name, recipient.Surname)
                });
            }

            MessageService.SaveMessage(message, _context);
            return responses;
        }
    }
}
