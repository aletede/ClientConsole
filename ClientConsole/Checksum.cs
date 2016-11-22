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
            using (MD5 md5 = MD5.Create())
            {
                using (FileStream stream = File.OpenRead(pathFile))
                {
                    byte[] checksumName = md5.ComputeHash(Encoding.ASCII.GetBytes(Path.GetFileName(pathFile)));
                    byte[] checksumData = md5.ComputeHash(stream);
                    Console.WriteLine(Convert.ToBase64String(mergeTwoHashes(md5, checksumName, checksumData)));
                }
            }
        }

        static private byte[] mergeTwoHashes(MD5 md5, byte[] hash1, byte[] hash2)
        {
            md5.TransformBlock(hash1, 0, hash1.Length, hash1, 0);
            md5.TransformFinalBlock(hash2, 0, hash2.Length);
            return md5.Hash;
            /*
            byte[] checksum = new byte[checksumName.Length + checksumData.Length];
            Buffer.BlockCopy(checksumName, 0, checksum, 0, checksumName.Length);
            Buffer.BlockCopy(checksumData, 0, checksum, checksumName.Length, checksumData.Length);
            byte[] checksumFile = md5.ComputeHash(checksum);
            Console.WriteLine("versione_2: {0}", Convert.ToBase64String(checksumFile));
            */
        }
    }
}
