namespace Cayley.Net.Dsl.Gremlin.Steps
{
    public static class Iterator
    {
        public static IGremlinQuery GremlinSkip(this IGremlinQuery query, int count)
        {
            IGremlinQuery newQuery = query.AddBlock(".drop({0})._()", count);
            return newQuery;
        }

        public static IGremlinQuery GremlinTake(this IGremlinQuery query, int count)
        {
            IGremlinQuery newQuery = query.AddBlock(".take({0})._()", count);
            return newQuery;
        }
    }
}