using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AIResumePicker.Service
{
    public class OpenAIService
    {
        private readonly string _endpoint = "";     //YOUR_OPENAI_ENDPOINT
        private readonly string _apiKey = "";       //YOUR_OPENAI_KEY
        private readonly HttpClient _httpClient;

        public OpenAIService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<bool> IsRelevantPdfAsync(string extractedText, string criteria)
        {
            var requestBody = new
            {
                model = "gpt-4",
                messages = new[]
                {
                new { role = "system", content = "You are an AI that determines if a document matches a given criteria." },
                new { role = "user", content = $"Criteria: {criteria}\n\nExtracted Text: {extractedText}\n\nDoes this Extracted Textt match the criteria? Answer with YES or NO." }
            },
                max_tokens = 10
            };

            var requestJson = JsonSerializer.Serialize(requestBody);
            Console.WriteLine("requestJson : " + requestJson);
            var requestContent = new StringContent(requestJson, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
            var response = await _httpClient.PostAsync($"{_endpoint}/v1/chat/completions", requestContent);
            Console.WriteLine("response : " + response);

            var responseJson = await response.Content.ReadAsStringAsync();
            Console.WriteLine("responseJson : " + responseJson);

            return responseJson.Contains("YES");
        }
    }
}