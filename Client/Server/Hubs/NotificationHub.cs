using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Infrastructure;
using System;
using System.Collections.Generic;
using Server.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

namespace Server.Hubs
{
    public class NotificationHub : Hub
    {
        public static NotificationHub instance = new NotificationHub();
        UserInfo userInfo = new UserInfo();
        public override async Task OnConnected()
        {
            string connectionId = Context.ConnectionId;
            NotificationHub context = new NotificationHub() { Context = Context };
            ConnectionManager.AddConnection(connectionId, context);
            instance.Context = Context;
            await base.OnConnected();
        }
        public override async Task OnDisconnected(bool stopCalled)
        {
            var connectionId = Context.ConnectionId;
            ConnectionManager.RemoveConnection(connectionId);
            await base.OnDisconnected(stopCalled);
        }
        public static void SendMessageToClient(string targetConnectionId)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.Client(targetConnectionId).updateMessages();
        }
    }
}



