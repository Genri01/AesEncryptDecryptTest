using System;
using System.IO;
using System.Security.Cryptography;

namespace AesEncryptDecryptTest
{
    public class AesEncryptDecryptHelper
    {

        private static byte[] Key = new byte[]
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29,
            30, 31, 32
        };

        private static byte[] IV = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
        public AesEncryptDecryptHelper(StorageRuleConnectionCryptoParam _cryptoParams)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.KeySize = _cryptoParams.keySize;
                aesAlg.Key = Convert.FromBase64String(_cryptoParams.Key);
                aesAlg.IV = Convert.FromBase64String(_cryptoParams.IV);
                aesAlg.Mode = _cryptoParams.CipherMode;
                aesAlg.Padding = _cryptoParams.PaddingMode;

                Encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                Decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            }
        }
        private ICryptoTransform Encryptor { get; set; }
        private ICryptoTransform Decryptor { get; set; }

        public string AesEncryptFile(string inputFilePath, string outputFilePath)
        {
            Console.WriteLine("AesEncryptDecryptHelper.AesEncryptFile - encrypt file: {0}", inputFilePath);

            using (var fsCrypt = new FileStream(outputFilePath, FileMode.Create))
            {
                using (var csEncrypt = new CryptoStream(fsCrypt, Encryptor, CryptoStreamMode.Write))
                {
                    using (var fsIn = new FileStream(inputFilePath, FileMode.Open))
                    {
                        int data;
                        while ((data = fsIn.ReadByte()) != -1)
                            csEncrypt.WriteByte((byte)data);

                        Console.WriteLine("AesEncryptDecryptHelper.AesEncryptFile - encrypt file successfully. Output File: {0}", outputFilePath);

                        return fsCrypt.Name;
                    }
                }
            }
        }

        public string AesDecryptFile(string inputFilePath, string outputFilePath)
        {
            Console.WriteLine("AesEncryptDecryptHelper.AesDecryptFile - decrypt file: {0}", inputFilePath);

            using (var msDecrypt = new FileStream(inputFilePath, FileMode.Open))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, Decryptor, CryptoStreamMode.Read))
                {
                    using (var fsOut = new FileStream(outputFilePath, FileMode.Create))
                    {
                        int data;
                        while ((data = csDecrypt.ReadByte()) != -1)
                            fsOut.WriteByte((byte)data);

                        Console.WriteLine("AesEncryptDecryptHelper.AesDecryptFile - decrypt file successfully Output File: {0}", outputFilePath);

                        return fsOut.Name;
                    }
                }
            }
        }
    }
}