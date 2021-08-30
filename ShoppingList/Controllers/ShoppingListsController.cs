using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Linq;

namespace Shopping_List.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class ShoppingListsController : ControllerBase
    {
        private readonly ICosmosDbService _cosmosDbService;

        public ShoppingListsController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [HttpGet]
        public async Task<IEnumerable<ShoppingList>> Get()
        {
            var items = await _cosmosDbService.GetItemsAsync("SELECT * FROM c");

            return items.Select(item => new ShoppingList()
                {
                    Id = item.Id,
                    Category = item.Category,
                    Name = item.Name,
                    Description = item.Description,
                    IsCompleted = item.IsCompleted,
                    DateTimeAdded = item.DateTimeAdded.Date.ToShortDateString()
                })
                .ToArray();
        }
    }
}
