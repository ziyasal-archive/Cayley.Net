using Cayley.Net.ApiModels;
using Cayley.Net.Dsl;
using Cayley.Net.Dsl.Gremlin;
using Cayley.Net.Dsl.Gremlin.Steps;

namespace Cayley.Net.Console
{
    public class Program
    {
        private static void Main()
        {
            ICayleyClient client = new CayleyClient("http://localhost:64210/api/v1/query/gremlin");
            IGraphObject g = new GraphObject();
            IGremlinQuery query = g.V().Has("name", "Casablanca")
                .Out("/film/film/starring")
                .Out("/film/performance/actor")
                .Out("name")
                .All();


            var filmToActor = g.Morphism().Out("/film/film/starring").Out("/film/performance/actor");
            IGremlinQuery queryWithMorphism = g.V()
                .Has("name", "Casablanca")
                .Follow(filmToActor)
                .Out("name")
                .All();
            CayleyResponse response = client.Send(query);
            CayleyResponse morpResponse = client.Send(queryWithMorphism);

            System.Console.WriteLine(response.RawText);

            System.Console.WriteLine("--------------------------------------------------------------------------------");

            System.Console.WriteLine(morpResponse.RawText);

            System.Console.ReadLine();
        }
    }
}