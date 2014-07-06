namespace Cayley.Net.Dsl.Gremlin.Steps
{
    public static class AggregateStep
    {
        public static IGremlinQuery AggregateV(this IGremlinQuery query, string variable)
        {
            IGremlinQuery newQuery = query.AddBlock(string.Format(".aggregate({0})", variable));
            newQuery.QueryDeclarations.Add(string.Format("{0} = [];", variable));
            newQuery = newQuery.PrependVariablesToBlock(newQuery);
            return newQuery;
        }
    }
}