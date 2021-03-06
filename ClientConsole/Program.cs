﻿using System;
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
            string directoryPath = @"C:\current";
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine("{0} is not a directory.", directoryPath);
                Environment.Exit(-1);
            }
            ProcessDirectory(directoryPath);
            // test md5 file
            //Checksum.computeHashFile(@"C:\current\prova.txt");
            //Checksum.computeHashFile(@"C:\current\prova2.txt");
            Console.ReadLine();
            Environment.Exit(-2);
            //String server = "192.168.1.5";
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
        
        // Process all files in the directory passed in, recurse on any directories 
        // that are found, and process the files they contain.
        public static void ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                Console.WriteLine(fileName);

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);
        }
    }
}
