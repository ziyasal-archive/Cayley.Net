using System.Collections.Generic;

namespace Cayley.Net.Dsl.Gremlin
{
    public interface IGremlinQuery
    {
        string QueryText { get; }
        IDictionary<string, object> QueryParameters { get; }
        IList<string> QueryDeclarations { get; }
    }
}