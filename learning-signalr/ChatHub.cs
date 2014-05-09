using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace learning_signalr
{
    public class ChatHub : Hub
    {
        public void Send(string name,string message) {
            Clients.All.broadCastMessage(name, message);
        }
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}