using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ITS.Vendrame.Scooter.Data.Models.ProtocolsHelper
{
    public class HttpClientModel
    {
        HttpClient httpClient;
        WebClient webClient;
        private readonly string _url = "http://d405e6ee2cd7.ngrok.io";

        public HttpClientModel()
        {
            webClient = new WebClient();
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_url);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.Timeout = TimeSpan.FromSeconds(30.0);
        }

        public void InsertDetection(Object sensorDetection)
        {
            var postUrl = _url + "/api/detection";
            var jsonToString = JsonSerializer.Serialize(sensorDetection);
            HttpContent content = new StringContent(jsonToString, Encoding.UTF8, "application/json");

            //webClient.UploadStringAsync(new Uri(_url + "/api/Detection"), jsonToString);
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            var response = webClient.UploadString(postUrl, "POST", jsonToString);

            //var response = httpClient.PostAsync("api/Detection", content).Result;
        }
    }
}
