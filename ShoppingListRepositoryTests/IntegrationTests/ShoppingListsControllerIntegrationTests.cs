using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace ShoppingListRepositoryTests
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Shopping_List;
    using Xunit;

    public class ShoppingListsControllerIntegrationTests
        : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public ShoppingListsControllerIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_Shopping_Lists()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/shoppingLists");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task Get_Shopping_List()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var shoppingListId = "35E40E50-7974-4AF3-8006-FA3168E8D6BA";
            var shoppingList = new ShoppingListModel
            {
                Id = shoppingListId,
                Category = "work",
                Name = "office supplies",
                Description = "this and that",
                IsCompleted = false,
                CreationDate = DateTime.Now
            };

            ////var shoppingListJson = new StringContent(
            ////    JsonSerializer.Serialize(new JsonTextWriter(), shoppingList),
            ////    Encoding.UTF8,
            ////    "application/json");

            ////var responseAdded = await client.PostAsync("/shoppingLists", shoppingListJson);

            //var response = await client.GetAsync($"/shoppingLists/{shoppingListId}");

            // Assert
            ////response.EnsureSuccessStatusCode(); // Status Code 200-299
            ////Assert.Equal("application/json; charset=utf-8",
            ////    response.Content.Headers.ContentType.ToString());
        }
    }
}