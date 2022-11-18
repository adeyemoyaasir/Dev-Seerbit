using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace paymentGateway.Models
{
    public class PaymentLinkResponse
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Data
    {
        public string code { get; set; }
        public Payments payments { get; set; }
        public string message { get; set; }
    }

    public class Payments
    {
        public string redirectLink { get; set; }
        public string paymentStatus { get; set; }
    }

    public class Root
    {
        public string status { get; set; }
        public Data data { get; set; }
    }


    }
}