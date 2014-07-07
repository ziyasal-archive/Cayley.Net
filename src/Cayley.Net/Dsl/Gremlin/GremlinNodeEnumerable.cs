using System.Collections.Generic;
using System.Diagnostics;

namespace Cayley.Net.Dsl.Gremlin
{
    [DebuggerDisplay("{DebugQueryText}")]
    internal class GremlinNodeEnumerable:  IGremlinQuery
    {
        private readonly IList<string> queryDeclarations;
        private readonly IDictionary<string, object> queryParameters;
        private readonly string queryText;

        public GremlinNodeEnumerable(IGremlinQuery query)
        {
            queryText = query.QueryText;
            queryParameters = query.QueryParameters;
            queryDeclarations = query.QueryDeclarations;
        }

        public string DebugQueryText
        {
            get { return this.Build(); }
        }

        string IGremlinQuery.QueryText
        {
            get { return queryText; }
        }

        IDictionary<string, object> IGremlinQuery.QueryParameters
        {
            get { return queryParameters; }
        }

        IList<string> IGremlinQuery.QueryDeclarations
        {
            get { return queryDeclarations; }
        }
    }
}