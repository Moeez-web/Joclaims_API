using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ShubeddakAPI.Hubs;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Microsoft.Owin.Security.OAuth;
using ShubeddakAPI.Providers;

[assembly: OwinStartup(typeof(ShubeddakAPI.Startup))]

namespace ShubeddakAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //OAuthServerOptions.AccessTokenExpireTimeSpan = TimeSpan.FromHours(hostSettings.ApiTokenExpirationTimeInHours);

            //OAuthOptions.AccessTokenExpireTimeSpan=TimeSpan.FromHours()

            //string PublicClientId = "self";


            ConfigureAuth(app);

            app.Map("/signalr", map => {
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration();
                map.RunSignalR(hubConfiguration);
            });
        }
    }
}
