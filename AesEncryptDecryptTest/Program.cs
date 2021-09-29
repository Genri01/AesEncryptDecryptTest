using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Tar;
using Newtonsoft.Json;

namespace AesEncryptDecryptTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a new instance of the Aes
            // class.  This generates a new key and initialization
            // vector (IV).
            // Encrypt the string to an array of bytes.

            //var aes = new AesEncryptDecryptHelper();

            //var encryptPath = aes.AesEncryptFile(@"D:\TestWav\1_03.wav", @"D:\TestWav\1_03_encrypt.wav");

            //Console.WriteLine("encryptPath: " + encryptPath);

            //// Decrypt the bytes to a string.
            //var decryptPath = aes.AesDecryptFile(@"D:\TestWav\1_03_encrypt.wav", @"D:\TestWav\1_03_decrypt.wav");
            //Console.WriteLine("decryptPath: " + decryptPath);


            ////AESCryptor.EncryptStringToBytes_Aes(@"D:\TestWav\1_03.wav", @"D:\TestWav\1_03_encrypt.wav", );


            ////var AesCryptHelper = new AesCryptHelper(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            //////AesCryptHelper.AES_Encrypt(@"D:\TestWav\1_03.wav", @"D:\TestWav\1_03_encrypt.wav");
            ////AesCryptHelper.AES_Decrypt(@"D:\TestWav\1_03_encrypt.wav", @"D:\TestWav\1_03_decrypt.wav");

            var str = "{\"algorithm\":\"AES\",\"cipher_mode\":1,\"enabled\":false,\"iv\":\"EO3h0EQFZ8cJ8R06DYab6g == \",\"key\":\"KFiO1QP3dbHJQUYg9\\/ vtjUqypFLTj5cXvbujfoYN5UY = \",\"key_size\":256,\"padding_mode\":3,\"thumbprint\":\"qwerty12345\"}";

            var cryptoParams = JsonConvert.DeserializeObject<CryptoParams>(str);

            FileStream dstStream = File.Create( @"D:\TestWav\shifr.tar");
            //using (StreamWriter writer = new StreamWriter(@"D:\TestWav\thumb.txt"))
            //{
            //    writer.Write(cryptoParams.Thumbprint);
            //}

            //using (TarArchive archive = TarArchive.CreateOutputTarArchive(dstStream))
            //{
            //    FileInfo fi = new FileInfo(@"D:\TestWav\1_03.wav");
            //    FileInfo f2 = new FileInfo(@"D:\TestWav\thumb.txt");

            //    using (StreamWriter sw = f2.AppendText())
            //    {
            //        sw.Write(cryptoParams.Thumbprint);
            //    }

            //    TarEntry newEntry = TarEntry.CreateEntryFromFile(@"D:\TestWav\1_03.wav");
            //    TarEntry thumbEntry = TarEntry.CreateEntryFromFile(@"D:\TestWav\thumb.txt");
            //    newEntry.Name = "TestName.vaw";
            //    newEntry.Size = fi.Length;
            //    newEntry.ModTime = fi.LastWriteTime;

            //    thumbEntry.Name = "123.txt";
            //    thumbEntry.Size = f2.Length;
            //    thumbEntry.ModTime = f2.LastWriteTime;

            //    archive.WriteEntry(newEntry, false);
            //    archive.WriteEntry(thumbEntry, false);
            //}

            //try
            //{
            //    using (var sr = new StreamReader(@"D:\TestWav\thumb.txt"))
            //    {
            //        string line;
            //        line = sr.ReadToEnd();
            //    }
            //}
            //catch
            //{

            //}
            //Console.ReadKey();

            //string Key = "012345678910111213141516171819202122";
            //string IV = "0123456789";

            //byte[] KeyBytes = Encoding.ASCII.GetBytes(Key);
            //var KeyBase64 = Convert.ToBase64String(KeyBytes);
            //byte[] KeyBase64Bytes = Encoding.ASCII.GetBytes(KeyBase64);

            //byte[] IVBytes = Encoding.ASCII.GetBytes(IV);
            //var IVBase64 = Convert.ToBase64String(IVBytes);
            //byte[] IVBase64Bytes = Encoding.ASCII.GetBytes(IVBase64);

            //var example = "E03h0EQFZ8cJ8R06DYab6g==";
            //byte[] example64Bytes = Encoding.ASCII.GetBytes(example);

            //var count = "MDEyMzQ1Njc4OTEwMTExMjEzMTQxNTE2MTcxODE5MjA".Length;

            //Key = "vTFBK1s7klXlsQG8JoGJZPs5m28ZyrHek40zwiRD3GI="
            //IV = "E03h0EQFZ8cJ8R06DYab6g=="

            var t = "\"algorithm\":\"AES\",\"cipher_mode\":1,\"enabled\":true,\"iv\":\"E03h0EQFZ8cJ8R06DYab6g==\",\"key\":\"vTFBK1s7klXlsQG8JoGJZPs5m28ZyrHek40zwiRD3GI=\",\"key_size\":256,\"padding_mode\":3,\"thumbprint\":null}";

            var cryptoParams2 = new StorageRuleConnectionCryptoParam {Algorithm = "AES", CipherMode = CipherMode.CBC, Key = "vTFBK1s7klXlsQG8JoGJZPs5m28ZyrHek40zwiRD3GI=", IV  = "E03h0EQFZ8cJ8R06DYab6g==", PaddingMode = PaddingMode.Zeros, keySize = 256};

            var _decryptor = new AesEncryptDecryptHelper(cryptoParams2);
            _decryptor.AesDecryptFile(@"D:\TestWav\121.wav", @"D:\TestWav\121_decrypt.wav");
            
            using (var rijndael = Rijndael.Create())
            {
                rijndael.GenerateKey();
                var key = Convert.ToBase64String(rijndael.Key);
                Console.WriteLine(key);
            }
        }
    }
}
