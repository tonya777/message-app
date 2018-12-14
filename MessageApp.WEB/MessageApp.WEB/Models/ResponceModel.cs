using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageApp.WEB.Models
{
    public class ResponceModel
    {
        public string MessageId { get; set; }

        public string Recipient { get; set; }

        public bool IsSent { get; set; }
    }
}
