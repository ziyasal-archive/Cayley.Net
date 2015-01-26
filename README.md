Cayley.Net
==========

.Net Client for an open-source graph database [Cayley](https://github.com/google/cayley)

[![Build status](https://ci.appveyor.com/api/projects/status/d4oynp026b2bnsya?svg=true)](https://ci.appveyor.com/project/ziyasal/cayley-net)

> Cayley is an open-source graph inspired by the graph database behind [Freebase](http://freebase.com/) and Google's [Knowledge Graph](http://www.google.com/insidesearch/features/search/knowledge.html). Its goal is to be a part of the developer's toolbox where [Linked Data](http://linkeddata.org/) and graph-shaped data (semantic webs, social networks, etc) in general are concerned.

##Sample

```csharp
  ICayleyClient client = new CayleyClient("http://localhost:64210/api/v1/query/gremlin");
  IGraphObject g = new GraphObject();

  // Start with only one vertex, the literal name "Humphrey Bogart", and retreive all of them.
  IGremlinQuery query=g.Vertex("Humphrey Bogart").All();
  CayleyResponse response = client.Send(query); //response.Content contains raw JSON data
  
  // `g` and `V` are synonyms for `graph` and `Vertex` respectively, as they are quite common.
  var query=g.V("Humphrey Bogart").All();
  CayleyResponse response = client.Send(query);
  
  // "Humphrey Bogart" is a name, but not an entity. Let's find the entities with this name in our dataset.
  // Follow links that are pointing In to our "Humphrey Bogart" node with the predicate "name".
  var query = g.V("Humphrey Bogart").In("name").All();
  CayleyResponse response = client.Send(query);
  
  // Notice that "name" is a generic predicate in our dataset. 
  // Starting with a movie gives a similar effect.
  var query = g.V("Casablanca").In("name").All();
  CayleyResponse response = client.Send(query);

  // Relatedly, we can ask the reverse; all ids with the name "Casablanca"
  var query = g.V().Has("name", "Casablanca").All();
  CayleyResponse response = client.Send(query);
  
  // Let's get the list of actors in the film
  var query = g.V().Has("name", "Casablanca")
                .Out("/film/film/starring")
                .Out("/film/performance/actor")
                .Out("name")
                .All();

  CayleyResponse response = client.Send(query);
  System.Console.WriteLine(response.Content);
  
  //But this is starting to get long. Let's use a morphism -- a pre-defined path stored in a variable -- as our linkage
   var filmToActor = g.Morphism().Out("/film/film/starring").Out("/film/performance/actor");
   var query = g.V()
                .Has("name", "Casablanca")
                .Follow(filmToActor)
                .Out("name")
                .All();
  CayleyResponse response = client.Send(query);
  System.Console.WriteLine(response.Content);

  //Add data programatically to the JSON result list. Can be any JSON type.
  var query = g.Emit(new {name = "John Doe", age = 41, isActor = true});
  CayleyResponse emitResponse = client.Send(query);
```


##Bugs
If you encounter a bug, performance issue, or malfunction, please add an [Issue](https://github.com/ziyasal/Cayley.Net/issues) with steps on how to reproduce the problem.


##TODO
- Improve Gremlin implementation (Basic steps implemented at the moment)
- Add more tests
- Strongly typed client
- Add more documentation
