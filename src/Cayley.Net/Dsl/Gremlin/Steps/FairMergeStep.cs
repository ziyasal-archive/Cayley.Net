namespace Cayley.Net.Dsl.Gremlin.Steps
{
    public static class FairMergeStep
    {
        public static IGremlinQuery FairMerge(this IGremlinQuery query)
        {
            IGremlinQuery newQuery = query.AddBlock(".fairMerge");
            return newQuery;
        }
    }
}