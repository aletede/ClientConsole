using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace ClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            String server = "192.168.1.5";
            int serverPort = 9000;
            String msg = "Funziona!";

            byte[] byteBuffer = Encoding.ASCII.GetBytes(msg);

            Socket sock = null;
            try
            {
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(server), serverPort);
                IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, serverPort);
                Console.WriteLine("Waiting for server up...");
                sock.Connect(serverEndPoint);
                Console.WriteLine("Connected to server... sending msg string");

                sock.Send(byteBuffer, 0, byteBuffer.Length, SocketFlags.None);

                Console.WriteLine("Sent {0} bytes to server...", byteBuffer.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                sock.Close();
            }

            Console.ReadLine();
        }
    }
}
