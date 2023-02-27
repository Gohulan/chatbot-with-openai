using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChatGPTExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string apiKey = "YOUR_API_KEY_HERE";
            string prompt = "Hello, how are you?";

            HttpClient httpClient = new HttpClient();
            string baseUrl = "https://api.openai.com/v1/engines/davinci-codex/completions";

            var requestData = new
            {
                prompt = prompt,
                max_tokens = 50
            };

            var jsonRequest = JsonSerializer.Serialize(requestData);

            var httpRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(baseUrl),
                Headers =
                {
                    { "Authorization", $"Bearer {apiKey}" }
                },
                Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json")
            };

            var httpResponse = await httpClient.SendAsync(httpRequest);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();

            var responseData = JsonSerializer.Deserialize<dynamic>(jsonResponse);
            string responseText = responseData.choices[0].text;

            Console.WriteLine(responseText);
        }
    }
}
