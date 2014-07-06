namespace Cayley.Net.Dsl.Gremlin.Steps
{
    public static class LoopStep
    {
        public static IGremlinQuery LoopV(this IGremlinQuery query, string label, uint loopCount)
        {
            IGremlinQuery newQuery = query.AddBlock(".loop({0}){{ it.loops < {1} }}", label, loopCount);
            return newQuery;
        }
    }
}