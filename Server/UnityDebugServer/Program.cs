using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UnityDebugServer
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 7777);
			
            Console.WriteLine("DEBUG Server started!");
            server.Start();

            while (true)
            {
                Console.WriteLine("Waiting... ");
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Connected...");
                NetworkStream stream = client.GetStream();

                while (client.Connected)
                {
                    byte[] data = new byte[256];
                    int bytes = stream.Read(data, 0, data.Length);

                    if (bytes != 0)
                    {

                        string message = Encoding.UTF8.GetString(data, 0, bytes);
                        Console.WriteLine(message);
                    }

                    if (client.Client.Poll(0, SelectMode.SelectRead))
                    {
                        byte[] buff = new byte[1];
                        if (client.Client.Receive(buff, SocketFlags.Peek) == 0)
                        {
                            client.Close();
                        }
                    }
                }
                Console.WriteLine("Disconnected...");
            }
        }
    }
}
