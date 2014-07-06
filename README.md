Cayley.Net
==========

.Net Client for an open-source graph database Cayley

##Sample

```csharp
  ICayleyClient client = new CayleyClient("http://localhost:64210/api/v1/query/gremlin");
  IGraphObject g = new GraphObject();
  
  // Start with only one vertex, the literal name "Humphrey Bogart", and retreive all of them.
  g.Vertex("Humphrey Bogart").All()
  
  // `g` and `V` are synonyms for `graph` and `Vertex` respectively, as they are quite common.
  g.V("Humphrey Bogart").All()
  
  // "Humphrey Bogart" is a name, but not an entity. Let's find the entities with this name in our dataset.
  // Follow links that are pointing In to our "Humphrey Bogart" node with the predicate "name".
  g.V("Humphrey Bogart").In("name").All()
  
  // Notice that "name" is a generic predicate in our dataset. 
  // Starting with a movie gives a similar effect.
  g.V("Casablanca").In("name").All()

  // Relatedly, we can ask the reverse; all ids with the name "Casablanca"
  g.V().Has("name", "Casablanca").All()

  // Let's get the list of actors in the film
  IGremlinQuery query = g.V().Has("name", "Casablanca")
                .Out("/film/film/starring")
                .Out("/film/performance/actor")
                .Out("name")
                .All();

  CayleyResponse response = client.Send(query);
  System.Console.WriteLine(response.RawText); //response.RawText contains raw JSON data
  
  //But this is starting to get long. Let's use a morphism -- a pre-defined path stored in a variable -- as our linkage
   var filmToActor = g.Morphism().Out("/film/film/starring").Out("/film/performance/actor");
   IGremlinQuery query = g.V()
                .Has("name", "Casablanca")
                .Follow(filmToActor)
                .Out("name")
                .All();
  CayleyResponse response = client.Send(query);
  System.Console.WriteLine(response.RawText); //response.RawText contains raw JSON data
```


##Bugs
If you encounter a bug, performance issue, or malfunction, please add an [Issue](https://github.com/ziyasal/Cayley.Net/issues) with steps on how to reproduce the problem.


##TODO
- Add more tests
- Strongly typed client
- Add more documentation
