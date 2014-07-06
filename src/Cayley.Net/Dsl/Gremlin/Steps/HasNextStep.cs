namespace Cayley.Net.Dsl.Gremlin.Steps
{
    public static class HasNextStep
    {
        public static IGremlinQuery GremlinHasNext(this IGremlinQuery query)
        {
            IGremlinQuery newQuery = query.AddBlock(".hasNext()");
            return newQuery;
        }
    }
}