using GuardNet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

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

        public async Task<HttpResponseMessage> GetHealthAsync()
        {
            return await GetAsync("/api/v1/health");
        }

        protected async Task<HttpResponseMessage> GetAsync(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);

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
