using BestStories.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;


namespace BestStories.Core.HttpServices
{
    public abstract class GenericHttpService : IGenericHttpService
    {
        protected readonly ILogger Logger;
        protected readonly HttpClient HttpClient;

        protected GenericHttpService(ILogger logger, HttpClient httpClient, string url)
        {
            Logger = logger;
            HttpClient = httpClient;
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpClient.BaseAddress = new Uri(url);
        }

        public async Task<HttpServiceResponse<T>> Get<T>(string addressSuffix, string id)
        {
            var response = await HttpClient.GetAsync($"{addressSuffix}/{id}");
            return await ProcessHttpResponse<T>(response);
        }

        public async Task<HttpServiceResponse<T>> Get<T>(string addressSuffix)
        {
            var response = await HttpClient.GetAsync($"{addressSuffix}");
            return await ProcessHttpResponse<T>(response);
        }

        public async Task<HttpServiceResponse<IEnumerable<T>>> GetAll<T>(string addressSuffix)
        {
            var response = await HttpClient.GetAsync(addressSuffix);
            return await ProcessHttpResponse<IEnumerable<T>>(response);
        }

        public async Task<HttpServiceResponse<T>> Post<T>(string addressSuffix, string jsonBody)
        {
            var response = await HttpClient.PostAsync(addressSuffix, CreateContent(jsonBody));
            return await ProcessHttpResponse<T>(response);
        }

        public async Task<HttpServiceResponse<T>> Put<T>(string addressSuffix, string id, string jsonBody)
        {
            var response = await HttpClient.PutAsync($"{addressSuffix}/{id}", CreateContent(jsonBody));
            return await ProcessHttpResponse<T>(response);
        }

        public async Task<HttpServiceResponse<T>> Delete<T>(string addressSuffix, string id)
        {
            var response = await HttpClient.DeleteAsync($"{addressSuffix}/{id}");
            return await ProcessHttpResponse<T>(response);
        }

        protected static StringContent CreateContent(string jsonBody)
        {
            return string.IsNullOrWhiteSpace(jsonBody)
                ? null
                : new StringContent(jsonBody, System.Text.Encoding.UTF8, "application/json");
        }

        protected static async Task<HttpServiceResponse<T>> ProcessHttpResponse<T>(HttpResponseMessage httpResponse)
        {
            httpResponse.EnsureSuccessStatusCode();

            var jsonSerializerOptions = new JsonSerializerOptions()
            { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var contentString = await httpResponse.Content.ReadAsStringAsync();
            var content = JsonSerializer.Deserialize<T>(contentString, jsonSerializerOptions);

            return new HttpServiceResponse<T>() { Content = content, Message = httpResponse };
        }
    }
}
