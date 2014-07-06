using System.Collections.Generic;

namespace Cayley.Net.Dsl.Gremlin
{
    internal class GremlinQuery : IGremlinQuery
    {
        private readonly IList<string> _queryDeclarations;
        private readonly IDictionary<string, object> _queryParameters;
        private readonly string _queryText;

        public GremlinQuery(string queryText, IDictionary<string, object> queryParameters, IList<string> declarations)
        {
            _queryText = queryText;
            _queryParameters = queryParameters;
            _queryDeclarations = declarations;
        }


        public string QueryText
        {
            get { return _queryText; }
        }

        public IDictionary<string, object> QueryParameters
        {
            get { return _queryParameters; }
        }

        public IList<string> QueryDeclarations
        {
            get { return _queryDeclarations; }
        }
    }
}