using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.IO;
using System.Security.Cryptography;

namespace RSA_Plugin
{
    [Plugin(PluginType.Cryptography)]
    public class MyPlugin : ICrypography
    {
        public string Name
        {
            get { return "DES Cipher Plugin"; }
        }

        public string Version
        {
            get { return "1.0.0"; }
        }

        public string Author
        {
            get { return "Me"; }
        }

        public string ChyperName
        {
            get { return "DES"; }
        }

        static string DESKey = "1111111";
        static string DESIV = "00000000";
        public byte[] Encrypt(byte[] plainTextBytes)
        {
            byte[] Key = Encoding.UTF8.GetBytes(DESKey);
            byte[] IV = Encoding.UTF8.GetBytes(DESIV);
            MemoryStream mStream = new MemoryStream();
            DES DESalg = DES.Create();
            CryptoStream cStream = new CryptoStream(mStream, DESalg.CreateEncryptor(Key, IV), CryptoStreamMode.Write);
            cStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cStream.FlushFinalBlock();
            byte[] cipherTextBytes = mStream.ToArray();
            cStream.Close();
            mStream.Close();
            return cipherTextBytes;
        }

        public byte[] Decrypt(byte[] Data)
        {
            byte[] Key = Encoding.UTF8.GetBytes(DESKey);
            byte[] IV = Encoding.UTF8.GetBytes(DESIV);
            MemoryStream mStream = new MemoryStream(Data);
            DES DESalg = DES.Create();
            CryptoStream cStream = new CryptoStream(mStream, DESalg.CreateDecryptor(Key, IV), CryptoStreamMode.Read);
            byte[] ret = new byte[Data.Length];
            int decryptedByteCount = cStream.Read(ret, 0, ret.Length);
            cStream.Close();
            mStream.Close();
            return ret;
        }
    }
}