using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetCoreServer;
using System.Net;

namespace ChatGPTProxy
{
   

    class Session : TcpSession
    {
        public Session(TcpServer server) : base(server) { }

        protected override void OnConnected()
        {
            Console.WriteLine($"Client connected: {Id}");
        }

        protected override void OnDisconnected()
        {
            Console.WriteLine($"Client disconnected: {Id}");
        }

        protected override async void OnReceived(byte[] buffer, long offset, long size)
        {
            string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
            if (!message.Contains("|"))
                return;
            //Console.WriteLine($"Received from {Id}: {message}");
            string[] parsed = message.Split('|');
            string result = await API.Ask(parsed[1], parsed[2]);
            byte[] resultBytes = Encoding.UTF8.GetBytes(result);
            SendAsync(resultBytes, 0, resultBytes.Length);
        }
    }

    class Server : TcpServer
    {
        public Server(IPAddress address, int port) : base(address, port) { }

        protected override TcpSession CreateSession() { return new Session(this); }
    }
}
