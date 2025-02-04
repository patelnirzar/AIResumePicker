using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace AIResumePicker.Service
{

    public class HuggingFaceService
    {

        private readonly string _apiKey = "";       //YOUR_HUGGINGFACE_API_KEY
        private readonly HttpClient _httpClient;

        public HuggingFaceService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<bool> IsRelevantPdfHuggingAsync(string extractedText, string criteria)
        {
            var requestBody = new
            {
                // inputs = $"Criteria: {criteria}\n\nExtracted Text: {extractedText}\n\nDoes this document match with the all Experience, Education and Skills criteria? Check strictly, say No if any one of criteria is not matching, Answer with YES or NO."
                inputs = $"Give me answer that this resume : {extractedText} match with this Job Criteria: {criteria} or not."
            };

            var requestJson = JsonSerializer.Serialize(requestBody);
            var requestContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

            var response = await _httpClient.PostAsync("https://api-inference.huggingface.co/models/mistralai/Mistral-7B-Instruct-v0.3", requestContent);
            // Console.WriteLine("response : " + response);

            var responseJson = await response.Content.ReadAsStringAsync();

            Console.WriteLine("responseJson : " + responseJson);

            if(responseJson.Contains("the resume does not") || responseJson.Contains("this resume does not"))
            {
                return false;
            }
            else{
                return true;
            }
            
        }
    }
}