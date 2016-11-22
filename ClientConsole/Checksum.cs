using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace ClientConsole
{
    class Checksum
    {
        static public void computeHashFile(string pathFile)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(pathFile))
                {
                    byte[] data = md5.ComputeHash(stream);
                    byte[] data2 = md5.ComputeHash(Encoding.ASCII.GetBytes(Path.GetFileName(pathFile)));
                    
                    StringBuilder sBuilder = new StringBuilder();
                    for (int i = 0; i < data2.Length; i++)
                    {
                        sBuilder.Append(data2[i].ToString("x2"));
                    }
                    for (int i = 0; i < data.Length; i++)
                    {
                        sBuilder.Append(data[i].ToString("x2"));
                    }
                    
                    Console.WriteLine(sBuilder.ToString());
                    //Console.WriteLine(Encoding.ASCII.GetString(md5.ComputeHash(stream)));
                }
            }
        }
    }
}
