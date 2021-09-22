using System;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;
using Xunit.Abstractions;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

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

        public ShoppingListsControllerIntegrationTests(WebApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
        {
            this.factory = factory;
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task Get_Shopping_Lists()
        {
            // Arrange
            var client = factory.CreateClient();

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
            // Arrange
            var client = factory.CreateClient();

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
            //testOutputHelper.WriteLine(json.Category as string);
            testOutputHelper.WriteLine(json.Value<string>("id"));
            //testOutputHelper.WriteLine(json as string);
        }
    }
}