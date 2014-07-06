using System.Collections.Generic;

namespace Cayley.Net.Dsl.Gremlin.Steps
{
    public static class IfThenElseStep
    {
        public static IGremlinQuery IfThenElse(this IGremlinQuery baseQuery, IGremlinQuery ifExpression,
            IGremlinQuery ifThen, IGremlinQuery ifElse)
        {
            ifThen = ifThen ?? new GremlinQuery(string.Empty, new Dictionary<string, object>(), new List<string>());
            ifElse = ifElse ?? new GremlinQuery(string.Empty, new Dictionary<string, object>(), new List<string>());


            baseQuery = baseQuery.AddIfThenElseBlock(".ifThenElse{{{0}}}{{{1}}}{{{2}}}", ifExpression, ifThen, ifElse);
            return baseQuery;

        }
    }
}