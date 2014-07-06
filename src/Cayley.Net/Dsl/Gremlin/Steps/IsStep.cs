namespace Cayley.Net.Dsl.Gremlin.Steps
{
    public static class IsStep
    {
        public static IGremlinQuery Is(this IGremlinQuery query, string node)
        {
            IGremlinQuery newQuery = query.AddBlock(string.Format(".Is('{0}')", node));
            return newQuery;
        }
    }
}