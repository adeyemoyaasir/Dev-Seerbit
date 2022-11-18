using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace paymentGateway.Models
{
    public class PaymentDetailsModel
    {
        public string PublicKey { get; set; }
        public string Amount { get; set; }
        public string Currency { get; set; }
        public string Country { get; set; }
        public string PaymentReference { get; set; }
        public string Email { get; set; }
        public string ProductId { get; set; }
        public string ProductDescription { get; set; }
        public string CallbackUrl { get; set; }
        public string Hash { get; set; }
        public string HashType { get; set; }
    }
}