using System.Net.Http;
using System.Threading.Tasks;
using GuardNet;
using Newtonsoft.Json;

namespace Codit.Exercises.DevOps.Tests.Extensions
{
    public static class HttpContentExtensions
    {
        /// <summary>
        /// Reads the content and deserializes into specified contract
        /// </summary>
        /// <typeparam name="TPayload">Type of expected payload</typeparam>
        /// <param name="httpContent">Content of HTTP communication</param>
        public static async Task<TPayload> ReadAsAsync<TPayload>(this HttpContent httpContent)
        {
            Guard.NotNull(httpContent, nameof(httpContent));

            var rawPayload = await httpContent.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TPayload>(rawPayload);
        }
    }
}