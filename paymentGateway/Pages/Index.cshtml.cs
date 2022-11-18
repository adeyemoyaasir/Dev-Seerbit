using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using paymentGateway.Models;
using paymentGateway.Models.PaymentLink;
using Newtonsoft.Json;

namespace paymentGateway.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private string token;
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

        var hashTask = CreatePaymentLink();
    }

    async Task CreatePaymentLink()
    {
        var url = "https://seerbitapi.com/api/v2/payments";

        var paymentLink = new Models.PaymentLink.Root();

        paymentLink.amount = "500";
        paymentLink.callbackUrl = "https://google.com";
        paymentLink.currency = "NGN";
        paymentLink.country = "NG";
        paymentLink.email = "eze@gmail.com";
        paymentLink.productId = "14093";
        paymentLink.paymentReference = "Y191090233047WZ73QN";
        paymentLink.productDescription = "leg badge";
        paymentLink.publicKey = "SBTESTPUBK_bEcvQMD2fF0rTVV68cuVpjfsTf113Ho4";
        paymentLink.hash = $"6a43b447eb69d2da3545f6d3fb6b66511679b6cd10ed59ead74f70c3251c3bac30b3429f6e6555a4b3c508332a76c5343d1fe85c276ee285a33dae6ca03769495e6cabd95ae98236a1a25bdfc9183d5b";
        paymentLink.hashType = "sha256";

        var paymentJson = System.Text.Json.JsonSerializer.Serialize(paymentLink);

        var data = new StringContent(paymentJson, Encoding.UTF8, "application/json");

        var client = new HttpClient();
        
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");

        var response = await client.PostAsync(url, data);

        var result = response.Content.ReadAsStringAsync().Result;

        var message = JsonConvert.DeserializeObject<PaymentLinkResponse.Root>(result);

        System.Console.WriteLine(message.data.payments.redirectLink);

        System.Console.WriteLine(result);
    }
}
