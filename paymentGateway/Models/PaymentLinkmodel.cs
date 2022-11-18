using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace paymentGateway.Models.PaymentLink
{
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

    public class Root
    {
        public string amount { get; set; }
        public string currency { get; set; }
        public string country { get; set; }
        public string callbackUrl { get; set; }
        public string publicKey { get; set; }
        public string productId { get; set; }
        public string productDescription { get; set; }
        public string email { get; set; }
        public string paymentReference { get; set; }
        public string hash { get; set; }
        public string hashType { get; set; }
        }
}