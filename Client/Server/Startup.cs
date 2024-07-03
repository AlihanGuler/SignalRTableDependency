using Microsoft.AspNet.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Owin;
using Server.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server
{
    public class Startup
    {
        [assembly: OwinStartup(typeof(Client))]

        public void Configuration(IAppBuilder app)
        {
            //app.MapSignalR("/notificationHub", new HubConfiguration());
            app.MapSignalR();
        }
            
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<DatabaseListener>();
            services.AddSingleton<NotificationHub>();
        }
    }
}