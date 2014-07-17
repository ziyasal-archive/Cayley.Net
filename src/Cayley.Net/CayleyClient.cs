using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Cayley.Net.ApiModels;
using Cayley.Net.Dsl.Gremlin;
using Cayley.Net.Extensions;

namespace Cayley.Net
{
    public class CayleyClient : ICayleyClient
    {
        private readonly string _basePath;

        public CayleyClient(string basePath)
        {
            _basePath = basePath;
        }

        public CayleyResponse Send(IGremlinQuery query)
        {
            string queryText = query.Build();
            return Send(queryText);
        }

        public CayleyResponse Send(string query)
        {
            HttpClient client = new HttpClient();
            var content = new StringContent(query);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            Task<HttpResponseMessage> task = client.PostAsync(_basePath, content);
            if (task.Result.IsSuccessStatusCode)
            {
                return new CayleyResponse { Content = task.Result.Content.ReadAsString() };
            }

            return default(CayleyResponse);
        }
    }
}