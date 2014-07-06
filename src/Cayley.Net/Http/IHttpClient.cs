using System.Net.Http;
using System.Threading.Tasks;

namespace Cayley.Net.Http
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}