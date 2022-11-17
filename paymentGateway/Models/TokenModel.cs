using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace paymentGateway.Models
{
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Data
    {
        public string code { get; set; }
        public EncryptedSecKey EncryptedSecKey { get; set; }
        public string message { get; set; }
    }

    public class EncryptedSecKey
    {
        public string encryptedKey { get; set; }
    }

    public class Root
    {
        public string status { get; set; }
        public Data data { get; set; }
    }
}