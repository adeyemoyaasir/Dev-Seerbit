using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace paymentGateway.Models
{
    public class CardModel
    {
        public string PublicKey { get; set; }
        public string Amount { get; set; }
        public string Fullname { get; set; }
        public string MobileNumber { get; set; }
        public string Currency { get; set; }
        public string Country { get; set; }
        public string PaymentReference { get; set; }
        public string Email { get; set; }
        public string PaymentType { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string Cvv { get; set; }
        public string pin { get; set; }
    }
}