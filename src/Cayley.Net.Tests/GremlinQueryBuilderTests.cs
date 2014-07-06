using Cayley.Net.Dsl;
using Cayley.Net.Dsl.Gremlin;
using Cayley.Net.Dsl.Gremlin.Steps;
using FluentAssertions;
using NUnit.Framework;

namespace Cayley.Net.Tests
{
    public class GremlinQueryBuilderTests : TestBase
    {
        public const string FOLLOW_WITH_MORPHISM_QUERY = @"g.V().Has('name', 'Casablanca')
                                                   .Follow(g.Morphism().Out('/film/film/starring').Out('/film/performance/actor'))
                                                   .Out('name').All()";

        private IGraphObject g;

        protected override void FinalizeSetUp()
        {
            g = new GraphObject();
        }

        [Test]
        public void VertexTest()
        {
            IGremlinQuery query = g.V();

            query.ToQueryText().Should().Be(GremlinContants.V);
        }

        [Test]
        public void MorphismTest()
        {
            IGremlinQuery query = g.M();

            query.ToQueryText().Should().Be(GremlinContants.M);
        }

        [Test]
        public void Vertext_HasStep_Test()
        {
            IGremlinQuery query = g.V().Has("name", "Casablanca");
            query.QueryText.Should().NotBeNullOrWhiteSpace();
            query.QueryText.Should().Be(@"g.V().Has('name', 'Casablanca')");
        }

        public void Vertext_Out_Step_Test()
        {
            IGremlinQuery query = g.V().Has("name", "Casablanca").Out("/film/film/starring").Out("/film/performance/actor").Out("name");
            query.QueryText.Should().NotBeNullOrWhiteSpace();
            query.QueryText.Should().Be(@"g.V().Has('name', 'Casablanca').Out('/film/film/starring').Out('/film/performance/actor').Out('name')");
        }

        [Test]
        public void Vertex_Basic_Query_Test()
        {
            IGremlinQuery query = g.V().Has("name", "Casablanca")
              .Out("/film/film/starring")
              .Out("/film/performance/actor")
              .Out("name")
              .All();
            query.QueryText.Should().NotBeNullOrWhiteSpace();
            query.QueryText.Should().Be(@"g.V().Has('name', 'Casablanca').Out('/film/film/starring').Out('/film/performance/actor').Out('name').All()");
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

            queryWithMorphism.QueryText.Should().NotBeNullOrWhiteSpace();
            queryWithMorphism.QueryText.Should().Be(FOLLOW_WITH_MORPHISM_QUERY);
        }
    }
}
