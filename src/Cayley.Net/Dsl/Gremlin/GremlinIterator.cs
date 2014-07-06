using System.Collections.Generic;

namespace Cayley.Net.Dsl.Gremlin
{
    public class GremlinIterator : IGremlinQuery
    {
        private readonly IList<string> queryDeclarations = new List<string>();
        private readonly IDictionary<string, object> queryParameters = new Dictionary<string, object>();


        public string QueryText
        {
            get { return "it"; }
        }

        public IDictionary<string, object> QueryParameters
        {
            get { return queryParameters; }
        }

        public IList<string> QueryDeclarations
        {
            get { return queryDeclarations; }
        }
    }
}