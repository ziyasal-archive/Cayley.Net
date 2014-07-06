namespace Cayley.Net.Dsl.Gremlin
{
    public static class Back
    {
        public static IGremlinQuery BackV(this IGremlinQuery query, string label)
        {
            IGremlinQuery newQuery = query.AddBlock(".back({0})", label);
            return newQuery;
        }
    }
}