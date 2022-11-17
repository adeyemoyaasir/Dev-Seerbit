using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using paymentGateway.Models;
using Newtonsoft.Json;

namespace paymentGateway.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private string token;

    public string Token { get => token; set => token = value; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        var newToken = CreateTokenAsync();
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

        Root responseData = JsonConvert.DeserializeObject<Root>(result);

        Token = responseData.data.EncryptedSecKey.encryptedKey;
        
        var makePayment = PaymentAsync();
    }

	async Task PaymentAsync()
	{
		var paymentUrl = "https://seerbitapi.com/api/v2/payments";

        var User = new Dictionary<string, string>();

        User.Add("publicKey", "SBTESTPUBK_bEcvQMD2fF0rTVV68cuVpjfsTf113Ho4");
        User.Add("amount", "500");
        User.Add("currency", "NGN");
        User.Add("country", "NG");
        User.Add("paymentReference", "643108207792124616573324");
        User.Add("email", "chikelubaobioraeze@gmail.com");
        User.Add("productId", "15013");
        User.Add("productDescription", "touch badge");
        User.Add("callbackUrl", "http://localhost:5011");
        User.Add("hash", "cfb5464ea21cce315ea72fb28f7ea45c4b61c443783eeff82dea98e57d445e15");
        User.Add("hashType", "sha256");
        
        var userJson = System.Text.Json.JsonSerializer.Serialize(User);
   
        var client = new HttpClient();

        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");

        var data = new StringContent(userJson, Encoding.UTF8, "application/json");

        var request = await client.PostAsync(paymentUrl, data);

        var result = request.Content.ReadAsStringAsync().Result;

        System.Console.WriteLine(result);
	}
}
