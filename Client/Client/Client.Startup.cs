using Microsoft.AspNet.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Owin;
using Server;
using Server.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace Client
{
    public class Startup
    {
        [assembly: OwinStartup(typeof(Client))] 
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
        public void ConfigureServices(IServiceCollection services)
        { 

            services.AddSingleton<NotificationHub>();
            services.AddSingleton<DatabaseListener>();
        }
    }
}