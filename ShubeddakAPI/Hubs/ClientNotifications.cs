using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShubeddakAPI.Hubs
{
    public class ClientNotification
    {

        public void BroadCastMessage(string message)
        {
            ServerHub.BroadCastMessage(message);

        }
        public void NotifyClient(string clientId, ClientNotification notify)
        {
            ServerHub.NotifyClient(clientId, notify);
        }

        public static void Send()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ServerHub>();
            context.Clients.All.displayStatus();
            //for update count
            context.Clients.All.changeCaptured("added");
        }


    }

}