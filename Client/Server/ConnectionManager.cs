using Server.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server
{
    public class ConnectionManager
    {
        public static Dictionary<string, NotificationHub> _userConnections = new Dictionary<string, NotificationHub>();

        public static void AddConnection(string userId, NotificationHub notificationHub)
        {
            lock (_userConnections)
            {
                if (!_userConnections.ContainsKey(userId))
                {
                    _userConnections[userId] = notificationHub;
                }
            }
        }
        public static void RemoveConnection(string userId)
        {
            lock (_userConnections)
            {
                if (_userConnections.ContainsKey(userId))
                {
                    _userConnections.Remove(userId);

                }
            }
        }
        public static NotificationHub GetConnections(string userId)
        {
            lock (_userConnections)
            {
                return _userConnections.ContainsKey(userId) ? _userConnections[userId] : null;
            }
        }
    }
}