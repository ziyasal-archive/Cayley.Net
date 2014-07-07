using System.Collections.Generic;
using System.Text;
using Cayley.Net.Dsl.Gremlin;
using Cayley.Net.Extensions;

namespace Cayley.Net.Dsl
{
    public class GraphObject : IGraphObject
    {
        public IGremlinQuery V()
        {
            return new GremlinQuery("g.V()", new Dictionary<string, object>(), new List<string>());
        }

        public IGremlinQuery V(params string[] nodeIds)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < nodeIds.Length; i++)
            {
                string nodeId = nodeIds[i];
                builder.AppendFormat(i == nodeIds.Length - 1 ? "'{0}'" : "'{0}',", nodeId);
            }
            return new GremlinQuery(string.Format("g.V({0})", builder), new Dictionary<string, object>(), new List<string>());
        }

        public IGremlinQuery M()
        {
            return new GremlinQuery("g.Morphism()", new Dictionary<string, object>(), new List<string>());
        }

        public IGremlinQuery Vertex()
        {
            return V();
        }

        public IGremlinQuery Vertex(params string[] nodeIds)
        {
            return V(nodeIds);
        }

        public IGremlinQuery Morphism()
        {
            return M();
        }

        public string Emit(object data)
        {
            return string.Format("g.Emit({0})", data.ToEmitString());
        }
    }
}
