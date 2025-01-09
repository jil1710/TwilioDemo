using AfricasTalkingCS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;


namespace TwilioDemo.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TwilioController : ControllerBase
    {


        private readonly ILogger<TwilioController> _logger;
        //private readonly AfricasTalkingGateway _gateway;


        public TwilioController(ILogger<TwilioController> logger)
        {
            _logger = logger;
            //_gateway = new AfricasTalkingGateway("sandbox", "atsk_e23cba472f4b741ff3155dba9af741f9488e8635a9aa0a48521c3316110986c2a09856ae");
        }

        
        private const string ApiKey = "atsk_e08e84f4f2b6959077be08e43f1f87c8e83e86a661f73ec2dfa2725ca6b1cdbb5771d58e"; // Your Africa's Talking API Key
        private const string Username = "sandbox"; // Your Africa's Talking Username
        private const string BaseUrl = "https://api.sandbox.africastalking.com/version1/";

        [HttpPost]
        public async Task<IActionResult> SendSmsAsync(string to, string message)
        {

            var url = $"{BaseUrl}messaging";
            var payload = $"username={Username}&to={to}&message={Uri.EscapeDataString(message)}";

            using (var client = new HttpClient())
            {
                // Encode username and API key into Basic Auth header
                var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Username}:{ApiKey}"));
                client.DefaultRequestHeaders.Add("Authorization", $"Basic {authToken}");

                var content = new StringContent(payload, Encoding.UTF8, "application/x-www-form-urlencoded");

                var response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return Ok($"Message sent successfully: {result}");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    return Ok($"Failed to send message: {response.StatusCode} - {response.ReasonPhrase} - {error}");
                }
            }


            //var url = $"{BaseUrl}messaging";
            //var payload = $"username={Username}&to={to}&message={Uri.EscapeDataString(message)}";

            //using (var client = new HttpClient())
            //{
            //    // Encode username and API key into Basic Auth header
            //    client.DefaultRequestHeaders.Add("apiKey", ApiKey);
            //    var payloadAuth = new { username = Username };
            //    var contentAuth = new StringContent(JsonConvert.SerializeObject(payloadAuth), Encoding.UTF8, "application/json");

            //    var response1 = await client.PostAsync("https://api.sandbox.africastalking.com/auth-token/generate", contentAuth);
            //    var responseData = await response1.Content.ReadAsStringAsync();
            //    var jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseData);
            //    var authToken = jsonResponse.token.ToString();

            //    client.DefaultRequestHeaders.Add("authToken", authToken);

            //    var content = new StringContent(payload, Encoding.UTF8, "application/x-www-form-urlencoded");

            //    var response = await client.PostAsync(url, content);

            //    if (response.IsSuccessStatusCode)
            //    {
            //        var result = await response.Content.ReadAsStringAsync();
            //        return Ok($"Message sent successfully: {result}");
            //    }
            //    else
            //    {
            //        var error = await response.Content.ReadAsStringAsync();
            //        return Ok($"Failed to send message: {response.StatusCode} - {response.ReasonPhrase} - {error}");
            //    }
            //}
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult RecieveMesage()
        {





            return Content("msg.ToString()", "application/xml");
        }
    }
}
