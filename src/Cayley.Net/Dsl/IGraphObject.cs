using Cayley.Net.Dsl.Gremlin;

namespace Cayley.Net.Dsl
{
    public interface IGraphObject
    {
        IGremlinQuery V();
        IGremlinQuery V(params string[] nodeIds);
        IGremlinQuery M();
        IGremlinQuery Vertex();
        IGremlinQuery Vertex(params string[] nodeIds);
        IGremlinQuery Morphism();
    }
}