using System.Net.Http;
using System.Threading.Tasks;

namespace Cayley.Net.Http
{
    public class HttpClientWrapper : IHttpClient
    {
        private readonly HttpClient _client;

        public HttpClientWrapper() : this(new HttpClient())
        {
        }

        public HttpClientWrapper(HttpClient client)
        {
            this._client = client;
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return _client.SendAsync(request);
        }
    }
}