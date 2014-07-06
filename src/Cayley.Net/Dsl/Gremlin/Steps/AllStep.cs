namespace Cayley.Net.Dsl.Gremlin.Steps
{
    public static class AllStep
    {
        public static IGremlinQuery All(this IGremlinQuery query)
        {
            IGremlinQuery newQuery = query.AddBlock(".All()");
            return newQuery;
        }
    }
}