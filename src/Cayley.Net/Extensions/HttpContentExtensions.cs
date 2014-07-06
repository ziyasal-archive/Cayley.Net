using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cayley.Net.Extensions
{
    internal static class HttpContentExtensions
    {
        public static string ReadAsString(this HttpContent content)
        {
            Task<string> readTask = content.ReadAsStringAsync();
            readTask.Wait();
            return readTask.Result;
        }

        public static T ReadAsJson<T>(this HttpContent content, IEnumerable<JsonConverter> jsonConverters)
            where T : new()
        {
            string stringContent = content.ReadAsString();
            return JsonConvert.DeserializeObject<T>(stringContent);
        }
    }
}