using Cayley.Net.Dsl.Gremlin;

namespace Cayley.Net.Dsl
{
    public interface IGremlinQueryBuilder
    {
        IGremlinQuery V();
        IGremlinQuery V(params string[] nodeIds);
        IGremlinQuery M();
        IGremlinQuery Vertex();
        IGremlinQuery Vertex(params string[] nodeIds);
        IGremlinQuery Morphism();
    }
}