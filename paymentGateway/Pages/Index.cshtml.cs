using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using paymentGateway.Models;
using paymentGateway.Models.PaymentLink;
using Newtonsoft.Json;
using System.Security.Cryptography;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Components.Routing;

namespace paymentGateway.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private string token;
    public string PublicKey { get; set; } = "SBTESTPUBK_bEcvQMD2fF0rTVV68cuVpjfsTf113Ho4";
    public string Hash { get; set; }

    public string Token { get => token; set => token = value; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        var newToken = CreateTokenAsync();
    }

    public void OpenLink(object sender, EventArgs e)
    {
        System.Console.WriteLine("Here");
    }

    async Task CreateTokenAsync()
    {
        var url = "https://seerbitapi.com/api/v2/encrypt/keys";

        var secretKey = "SBTESTSECK_WvbokHl9VOja1e5nmLxgldeUtpTJj4eRB4alYgja.SBTESTPUBK_bEcvQMD2fF0rTVV68cuVpjfsTf113Ho4";

        var secret = new Dictionary<string, string>();

        secret.Add("key", secretKey);

        var jsonSecret = System.Text.Json.JsonSerializer.Serialize(secret);

        var data = new StringContent(jsonSecret, Encoding.UTF8, "application/json");


        var client = new HttpClient();

        var respons = await client.PostAsync(url, data);

        var result = respons.Content.ReadAsStringAsync().Result;

        var responseData = JsonConvert.DeserializeObject<Models.Root>(result);

        Token = responseData.data.EncryptedSecKey.encryptedKey;

        var payment = PaymentAsync();
    }

    async Task PaymentAsync()
    {
        var url = "https://seerbitapi.com/api/v2/payments";

        var paymentDoc = new Dictionary<string, string>();

        paymentDoc.Add("publicKey", PublicKey);
        paymentDoc.Add("amount", "500");
        paymentDoc.Add("email", "eze@gmail.com");
        paymentDoc.Add("country", "NG");
        paymentDoc.Add("currency", "NGN");
        paymentDoc.Add("paymentReference", $"{PaymentRef()}");

        var jsonPayment = System.Text.Json.JsonSerializer.Serialize(paymentDoc);

        var data = new StringContent(jsonPayment, Encoding.UTF8, "application/json");

        var client = new HttpClient();

        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");

        var response = await client.PostAsync(url, data);

        var result = response.Content.ReadAsStringAsync().Result;

        var message = JsonConvert.DeserializeObject<PaymentLinkResponse.Root>(result);

        Console.WriteLine(message.data.payments.redirectLink);


    }

    private string PaymentRef()
    {
        Random ran = new Random();

        String b = "abcdefghijklmnopqrstuvwxyz0123456789";

        int length = 6;

        String random = "";

        for (int i = 0; i < length; i++)
        {
            int a = ran.Next(b.Length); //string.Lenght gets the size of string
            random = random + b.ElementAt(a);
        }


        return random;
    }
}
