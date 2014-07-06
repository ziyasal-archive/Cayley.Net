namespace Cayley.Net.Dsl.Gremlin.Steps
{
    public static class StoreStep
    {
        public static IGremlinQuery StoreV(this IGremlinQuery query, string variable)
        {
            IGremlinQuery newQuery = query.AddBlock(string.Format(".store({0})", variable));
            return newQuery;
        }
    }
}