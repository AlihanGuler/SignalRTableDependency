using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Models
{
    public class Messages
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string ConnectionId { get; set; }
    }
}