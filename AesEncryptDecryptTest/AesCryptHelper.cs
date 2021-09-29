using System;
using System.IO;
using System.Security.Cryptography;

namespace AesEncryptDecryptTest
{
    public class AesCryptHelper
    {
        private static readonly byte[] m_saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        private static readonly RijndaelManaged AES = new RijndaelManaged { KeySize = 256, BlockSize = 128 };
        public AesCryptHelper(byte[] passwordBytes)
        {
            var key = new Rfc2898DeriveBytes(passwordBytes, m_saltBytes, 1000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);
            AES.Padding = PaddingMode.Zeros;
            AES.Mode = CipherMode.CBC;
        }

        public void AES_Encrypt(string inputFile, string outputFile)
        {
            Console.WriteLine("Start encrypt");
            string cryptFile = outputFile;
            
            FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);
            
            CryptoStream cs = new CryptoStream(fsCrypt,
                 AES.CreateEncryptor(),
                CryptoStreamMode.Write);

            FileStream fsIn = new FileStream(inputFile, FileMode.Open);

            int data;
            while ((data = fsIn.ReadByte()) != -1)
                cs.WriteByte((byte)data);


            fsIn.Close();
            cs.Close();
            fsCrypt.Close();
            Console.WriteLine("End encrypt");
        }

        public void AES_Decrypt(string inputFile, string outputFile)
        {
            Console.WriteLine("Start decrypt");
            FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

            CryptoStream cs = new CryptoStream(fsCrypt,
                AES.CreateDecryptor(),
                CryptoStreamMode.Read);

            FileStream fsOut = new FileStream(outputFile, FileMode.Create);

            int data;
            while ((data = cs.ReadByte()) != -1)
                fsOut.WriteByte((byte)data);

            fsOut.Close();
            cs.Close();
            fsCrypt.Close();
            Console.WriteLine("End decrypt");

        }
    }
}

