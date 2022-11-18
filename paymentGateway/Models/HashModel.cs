using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace paymentGateway.Models
{
    public class HashModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Data
    {
        public string code { get; set; }
        public string message { get; set; }
        public Hash hash { get; set; }
    }

    public class Hash
    {
        public string hash { get; set; }
    }

    public class Root
    {
        public string status { get; set; }
        public Data data { get; set; }
    }


    }
}