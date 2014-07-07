namespace Cayley.Net.Dsl.Gremlin.Steps
{
    public static class FollowStep
    {
        public static IGremlinQuery Follow(this IGremlinQuery query, IGremlinQuery morphismQuery)
        {
            IGremlinQuery newQuery = query.AddBlock(string.Format(".Follow({0})", morphismQuery.Build()));
            return newQuery;
        } 
        
        public static IGremlinQuery Follow(this IGremlinQuery query, string morphismQuery)
        {
            IGremlinQuery newQuery = query.AddBlock(string.Format(".Follow({0})", morphismQuery));
            return newQuery;
        }
    }
}