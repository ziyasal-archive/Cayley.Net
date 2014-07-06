using System.Net;
using Cayley.Net.ApiModels;
using Cayley.Net.Dsl.Gremlin;
using EasyHttp.Http;

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
            string queryText = query.ToQueryText();
            var http = new HttpClient(_basePath);
            http.Request.Accept = HttpContentTypes.ApplicationXWwwFormUrlEncoded;
            HttpResponse httpResponse = http.Post("", queryText, "text/plain");

            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                return new CayleyResponse { RawText = httpResponse.RawText };
            }

            return default(CayleyResponse);
        }
    }
}