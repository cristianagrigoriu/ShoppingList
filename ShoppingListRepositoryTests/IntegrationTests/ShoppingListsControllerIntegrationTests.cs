using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Xunit.Abstractions;

namespace ShoppingListRepositoryTests
{
    using System.Net;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Shopping_List;
    using Xunit;

    public class ShoppingListsControllerIntegrationTests
        : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;
        private readonly ITestOutputHelper testOutputHelper;
        private readonly HttpClient client;

        public ShoppingListsControllerIntegrationTests(WebApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
        {
            this.factory = factory;
            this.testOutputHelper = testOutputHelper;
            this.client = factory.CreateClient();
        }

        [SetUp]

        [Fact]
        public async Task Get_Shopping_Lists()
        {
            // Act
            var response = await client.GetAsync("/shoppingLists");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task Post_Shopping_List()
        {
            // Act
            var response = await client.PostAsJsonAsync("/shoppingLists", new
            {
                Category = "work",
                Name = "office supplies",
                Description = "this and that"
            });

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var content = await response.Content.ReadAsStringAsync();
            JObject json = JsonConvert.DeserializeObject<JObject>(content);
            testOutputHelper.WriteLine(json.Value<string>("id"));
        }

        [Fact]
        public async Task When_posting_shopping_list_Should_retrieve_newly_list_by_returned_id()
        {
            // Act
            var response = await client.PostAsJsonAsync("/shoppingLists", new
            {
                Category = "work",
                Name = "office supplies",
                Description = "this and that"
            });

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var content = await response.Content.ReadAsStringAsync();
            JObject json = JsonConvert.DeserializeObject<JObject>(content);
            var id = json.Value<string>("id");
            var listResponse = await client.GetAsync($"/shoppingLists/{id}");
            var listCOntent = await listResponse.Content.ReadAsStringAsync();
            JObject listJson = JsonConvert.DeserializeObject<JObject>(listCOntent);
            listJson.Value<string>("description").Should().Be("this and that");
            listJson.Value<string>("id").Should().Be(id);
        }

        [Fact]
        public async Task When_updating_shopping_list_Should_retrieve_updated_list()
        {
            var createResponse = await client.PostAsJsonAsync("/shoppingLists", new
            {
                Category = "work",
                Name = "office supplies",
                Description = "this and that"
            });
            var createContent = await createResponse.Content.ReadAsStringAsync();
            JObject createJson = JsonConvert.DeserializeObject<JObject>(createContent);
            var id = createJson.Value<string>("id");

            // Act
            var response = await client.PutAsJsonAsync($"/shoppingLists/{id}", new
            {
                Category = "work 2",
                Name = "office supplies",
                Description = "this and that"
            });

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            JObject json = JsonConvert.DeserializeObject<JObject>(content);
            json.Value<string>("category").Should().Be("work 2");
        }
    }
}