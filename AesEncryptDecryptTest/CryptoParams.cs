using System.Security.Cryptography;
using Newtonsoft.Json;

namespace AesEncryptDecryptTest
{
    public class CryptoParams
    {
        [JsonProperty("algorithm")]
        public string Algorithm { get; set; }

        [JsonProperty("cipher_mode")]
        public CipherMode CipherMode { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("iv")]
        public string IV { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("key_size")]
        public int keySize { get; set; }

        [JsonProperty("padding_mode")]
        public PaddingMode PaddingMode { get; set; }

        [JsonProperty("thumbprint")]
        public string Thumbprint { get; set; }
    }
}
