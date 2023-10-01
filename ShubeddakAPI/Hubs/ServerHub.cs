using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;


namespace ShubeddakAPI.Hubs
{
    public class ServerHub : Hub
    {
        public void RegisterForNotification(string clientId)
        {
            Groups.Add(Context.ConnectionId, clientId);
        }
        public static void NotifyClient(string clientId, object data)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ServerHub>();

            hubContext.Clients.Group(clientId).clientNotified(data);

            //hubContext.Clients.User(clientId).clientNotified(data);
        }
        public static void BroadCastMessage(string message)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ServerHub>();
            hubContext.Clients.All.getMessageFromServer(message);
        }
    }

}