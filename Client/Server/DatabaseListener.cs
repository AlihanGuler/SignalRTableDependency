using Microsoft.AspNet.SignalR;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Server.Hubs;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;

namespace Server
{
    public class DatabaseListener
    {
        readonly string _connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private  NotificationHub _notHub;
        //UserInfo user = new UserInfo();
        public DatabaseListener(NotificationHub notHub)
        {
            _notHub = notHub;
        }
        public IEnumerable<Messages> GetAllMessages()
        {
            var messages = new List<Messages>();
            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();
                SqlDependency.Start(_connString);
                    using (var command = new SqlCommand(@"SELECT [Message] FROM [dbo].[Messages] WHERE ConnectionId = @ConnectionId", connection))
                    {
                    string userEmail = GetUserEmailByConnectionId(_notHub.Context.ConnectionId);
                    command.Parameters.AddWithValue("@ConnectionId", userEmail);
                    var dependency = new SqlDependency(command);
                        dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                        if (connection.State == ConnectionState.Closed)
                            connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                messages.Add(new Messages
                                {
                                    Message = (string)reader["Message"],
                                });
                            }
                        }
                }
                return messages;
            }
        }
        private async void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (_notHub != null && _notHub.Context != null)
            {
                if (e.Type == SqlNotificationType.Change)
                {
                    var connectionId = _notHub.Context.ConnectionId;
                    string userId = ConnectionManager.GetConnections(connectionId).ToString();
                    if (!string.IsNullOrEmpty(userId))
                    {
                      NotificationHub.SendMessageToClient(connectionId);
                    }
                }
            }
        }
        private string GetUserEmailByConnectionId(string connectionId)
        {
            string userEmail = null;
            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();
                using (var command = new SqlCommand(@"SELECT Email FROM [dbo].[EMails] WHERE ConnectionId = @ConnectionId", connection))
                {
                    command.Parameters.AddWithValue("@ConnectionId", connectionId);
                    userEmail = (string)command.ExecuteScalar();
                }
            }
            return userEmail;
        }
    }
}



