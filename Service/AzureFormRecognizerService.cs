using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AIResumePicker.Service
{
    public class AzureFormRecognizerService
    {
        private readonly string _endpoint = "";    //YOUR_FORM_RECOGNIZER_ENDPOINT
        private readonly string _apiKey = "";   //YOUR_FORM_RECOGNIZER_KEY
        private readonly DocumentAnalysisClient _client;

        public AzureFormRecognizerService()
        {
            var credential = new AzureKeyCredential(_apiKey);
            _client = new DocumentAnalysisClient(new Uri(_endpoint), credential);
        }

        public async Task<string> ExtractTextFromPdfAsync(string filePath)
        {
            using var stream = File.OpenRead(filePath);
            var operation = await _client.AnalyzeDocumentAsync(WaitUntil.Completed, "prebuilt-read", stream);

            var extractedText = string.Join(" ", operation.Value.Content);
            return extractedText;
        }
    }
}