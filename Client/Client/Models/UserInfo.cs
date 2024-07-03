using Server;
using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;

namespace Client.Models
{
    public class UserInfo
    {
        public int UserId { get; set; }
        public string UserMail { get; set; }
        public string ConnectionId { get; set; }
        public DatabaseListener DatabaseListener { get; set; }
    }
}