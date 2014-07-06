namespace Cayley.Net.Dsl.Gremlin.Steps
{
    public static class TagStep
    {
        public static IGremlinQuery Tag(this IGremlinQuery query, string tag)
        {
            IGremlinQuery newQuery = query.AddBlock(string.Format(".Tag('{0}')", tag));
            return newQuery;
        }
    }
}