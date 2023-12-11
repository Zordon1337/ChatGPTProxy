using ChatGPTProxy;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class Server
{
    /*
     * Hi! Thank you for looking into this code
     * it's not probably the best code, i mean
     * i don't really know if it's stable enough
     * to be used for production, i made it "as it is"
     * it's just an concept to show that you can use ChatGPT
     * on older hardware 
     * 
     * Disclaimer: reading this code may cause brain damage, so read it at own risk
     */
    static void Main()
    {
        StartTcpServer();
        Console.ReadLine(); 
    }

    static void StartTcpServer()
    {
        int port = 1337;
        var server = new ChatGPTProxy.Server(IPAddress.Any, port);

        if (server.Start())
            Console.WriteLine($"TCP server started on port {port}");
        else
            Console.WriteLine("Failed to start TCP server");

        Console.WriteLine("Press Enter to stop the server");
        Console.ReadLine();

        server.Stop();
    }
}


