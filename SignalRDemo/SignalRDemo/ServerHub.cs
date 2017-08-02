using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SignalRDemo
{
    public class ServerHub : Hub
    {
        private static readonly char[] Constant =
     {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
            'w', 'x', 'y', 'z',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V',
            'W', 'X', 'Y', 'Z'
        };
        public void Hello()
        {
            Clients.All.hello();
        }

        /// <summary>
        /// 供客户端调用的服务端代码
        /// </summary>
        /// <param name="message"></param>
        public void Send(string message)
        {
            var name = GenerateRandomName(4);
            
            // 调用所有客户端的SendMessage方法
            Clients.All.sendMessage(name, message);
        }

        public override Task OnConnected()
        {
            Trace.WriteLine("客户端建立连接");
            return base.OnConnected();
        }

        /// <summary>
        /// 产生随机用户名函数
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateRandomName(int length)
        {
            var newRandom=new StringBuilder(62);
            var rd=new Random();
            for (int i = 0; i < length; i++)
            {
                newRandom.Append(Constant[rd.Next(62)]);
            }
            return newRandom.ToString();
        }
    }
}