using Cayley.Net.Dsl;
using Cayley.Net.Dsl.Gremlin;
using Cayley.Net.Dsl.Gremlin.Steps;
using Cayley.Net.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace Cayley.Net.Tests
{
    public class GremlinQueryTests : TestBase
    {
        private IGraphObject g;

        protected override void FinalizeSetUp()
        {
            g = new GraphObject();
        }

        [Test]
        public void VertexTest()
        {
            IGremlinQuery query = g.V();

            query.Build().Should().Be(GremlinContants.V);
        }

        [Test]
        public void MorphismTest()
        {
            IGremlinQuery query = g.M();

            query.Build().Should().Be(GremlinContants.M);
        }

        [Test]
        public void Vertext_HasStep_Test()
        {
            IGremlinQuery query = g.V().Has("name", "Casablanca");
            query.Build().Should().NotBeNullOrWhiteSpace();
            query.Build().Should().Be(@"g.V().Has('name','Casablanca')");
        }

        public void Vertext_Out_Step_Test()
        {
            IGremlinQuery query = g.V().Has("name", "Casablanca").Out("/film/film/starring").Out("/film/performance/actor").Out("name");
            query.Build().Should().NotBeNullOrWhiteSpace();
            query.Build().Should().Be(@"g.V().Has('name','Casablanca').Out('/film/film/starring').Out('/film/performance/actor').Out('name')");
        }

        [Test]
        public void Vertex_Basic_Query_Test()
        {
            IGremlinQuery query = g.V().Has("name", "Casablanca")
              .Out("/film/film/starring")
              .Out("/film/performance/actor")
              .Out("name")
              .All();
            query.Build().Should().NotBeNullOrWhiteSpace();
            query.Build().Should().Be(@"g.V().Has('name','Casablanca').Out('/film/film/starring').Out('/film/performance/actor').Out('name').All()");
        }

        [Test]
        public void Follow_With_Morphism_Path_Test()
        {
            var filmToActor = g.Morphism().Out("/film/film/starring").Out("/film/performance/actor");
            IGremlinQuery queryWithMorphism = g.V()
               .Has("name", "Casablanca")
               .Follow(filmToActor)
               .Out("name")
               .All();

            queryWithMorphism.Build().Should().NotBeNullOrWhiteSpace();
            queryWithMorphism.Build().Should().Be("g.V().Has('name','Casablanca').Follow(g.Morphism().Out('/film/film/starring').Out('/film/performance/actor')).Out('name').All()");
        }

        [Test]
        public void Emit_Test()
        {
            var data = new { name = "ziya", age = 25, hasRoom = true };
            string result = g.Emit(data);

            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Be(string.Format("g.Emit({0})", data.ToEmitString()));
        }
    }
}
