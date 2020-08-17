using GuardNet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Codit.Exercises.DevSecOps.Core.Model;
using Newtonsoft.Json;

namespace Codit.Exercises.DevOps.Tests
{
    public class WarehouseClient
    {
        protected HttpClient HttpClient { get; }
        protected ILogger Logger { get; }

        public WarehouseClient(IConfiguration configuration, ILogger logger)
        {
            Guard.NotNull(configuration, nameof(configuration));
            Guard.NotNull(logger, nameof(logger));

            var baseUrl = configuration["API:BaseUrl"];
            logger.LogInformation("Base URL for API is '{Url}'", baseUrl);

            HttpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
            Logger = logger;
        }

        public async Task<HttpResponseMessage> GetAllProductsAsync()
        {
            return await GetAsync("/api/v1/products");
        }

        public async Task<HttpResponseMessage> GetHealthAsync()
        {
            return await GetAsync("/api/v1/health");
        }

        public async Task<HttpResponseMessage> NewProductAsync(Product product)
        {
            return await PostAsync("/api/v1/products", product);
        }

        protected async Task<HttpResponseMessage> GetAsync(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            var response = await SendRequestAsync(request);

            return response;
        }

        protected async Task<HttpResponseMessage> PostAsync(string uri, object payload)
        {
            var rawRequestPayload = JsonConvert.SerializeObject(payload);
            var request = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = new StringContent(rawRequestPayload, Encoding.UTF8, "application/json")
            };

            var response = await SendRequestAsync(request);

            return response;
        }

        private async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request)
        {
            var stopwatch = Stopwatch.StartNew();
            var response = await HttpClient.SendAsync(request);
            stopwatch.Stop();

            var context = new Dictionary<string, object>();
            try
            {
                var rawResponse = await response.Content.ReadAsStringAsync();
                context.Add("Body", rawResponse);
            }
            finally
            {
                Logger.LogRequest(request, response, stopwatch.Elapsed, context);
            }

            return response;
        }
    }
}
