using Cayley.Net.ApiModels;
using Cayley.Net.Dsl.Gremlin;

namespace Cayley.Net
{
    public interface ICayleyClient
    {
     
        CayleyResponse Send(IGremlinQuery query);
    }
}