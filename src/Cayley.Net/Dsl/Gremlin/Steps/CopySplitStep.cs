namespace Cayley.Net.Dsl.Gremlin.Steps
{
    public static class CopySplitStep
    {
        public static IGremlinQuery CopySplitV(this IGremlinQuery baseQuery,
            params IGremlinQuery[] queries)
        {
            baseQuery = baseQuery.AddCopySplitBlock("._.copySplit({0}, {1})", queries);
            return baseQuery;
        }
    }
}