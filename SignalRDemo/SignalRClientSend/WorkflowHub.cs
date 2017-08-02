using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SignalRClientSend
{
    public class WorkflowHub : Hub
    {

        private IList<string> userList = UserInfo.userList;

        public void Hello()
        {
            Clients.All.hello();
        }

        private readonly static Dictionary<string,string> _connections=new Dictionary<string, string>();

        public void SendByGroup(string name1, string name2)
        {
            var id = _connections[name2];
            Clients.Client(_connections[name2])
                .SendMessage("来自用户" + name1 + " " + DateTime.Now.ToString("yyyy-MMMM-dd hh:mm:ss") + "的消息");

        }

        public void SendLogin(string name)
        {
            if (!userList.Contains(name))
            {
                userList.Add(name);
                _connections.Add(name,Context.ConnectionId);
            }
            else
            {
                _connections[name] = Context.ConnectionId;
            }
            Clients.All.LoginUser(userList);
        }
    }
}