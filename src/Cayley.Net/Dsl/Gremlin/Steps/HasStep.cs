namespace Cayley.Net.Dsl.Gremlin.Steps
{
    public static class HasStep
    {
        public static IGremlinQuery Has(this IGremlinQuery query, string variable, string value)
        {
            IGremlinQuery newQuery = query.AddBlock(string.Format(".Has('{0}','{1}')", variable, value));
            return newQuery;
        }
    }
}