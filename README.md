Cayley.Net
==========

.Net Client for an open-source graph database Cayley

##Sample

```csharp
  // Let's get the list of actors in the film
  ICayleyClient client = new CayleyClient("http://localhost:64210/api/v1/query/gremlin");
  IGremlinQueryBuilder g = new GremlinQueryBuilder();
  IGremlinQuery query = g.V().Has("name", "Casablanca")
                .Out("/film/film/starring")
                .Out("/film/performance/actor")
                .Out("name")
                .All();

  CayleyResponse response = client.Send(query);
  System.Console.WriteLine(response.RawText); //response.RawText contains raw JSON data
  
  // Let's use a morphism -- a pre-defined path stored in a variable -- as our linkage
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
