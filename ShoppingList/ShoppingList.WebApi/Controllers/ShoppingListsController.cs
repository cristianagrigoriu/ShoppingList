namespace Shopping_List.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class ShoppingListsController : ControllerBase
    {
        private readonly IShoppingListsRepository shoppingListsRepository;

        public ShoppingListsController(IShoppingListsRepository shoppingListsRepository)
        {
            this.shoppingListsRepository = shoppingListsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Shopping_List.ShoppingList>> Get()
        {
            var items = await shoppingListsRepository.GetAllItems();

            return items.Select(item => new Shopping_List.ShoppingList()
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

        [HttpGet("{shoppingListId}")]
        public async Task<ActionResult<Shopping_List.ShoppingList>> Get([FromRoute]string shoppingListId)
        {
            var item = await shoppingListsRepository.GetItemAsync(shoppingListId);

            if (item == null)
            {
                return NotFound();
            }

            return new Shopping_List.ShoppingList
            {
                Id = item.Id,
                Category = item.Category,
                Name = item.Name,
                Description = item.Description,
                IsCompleted = item.IsCompleted,
                DateTimeAdded = item.DateTimeAdded.Date.ToShortDateString()
            };
        }

        [HttpPost]
        public ActionResult<Shopping_List.ShoppingList> AddShoppingList(ShoppingList shoppingList)
        {
            this.shoppingListsRepository.AddItemAsync(shoppingList);

            return Created("", shoppingList);
        }

        [HttpPut("{id}")]
        public ActionResult<Shopping_List.ShoppingList> UpdateShoppingList([FromRoute] string id, ShoppingList updatedShoppingList)
        {
            var existingShoppingList = this.shoppingListsRepository.GetItemAsync(id);
            if (existingShoppingList == null)
            {
                return NotFound($"Could not find shopping list with id = {id}");
            }

            this.shoppingListsRepository.UpdateItemAsync(id, updatedShoppingList);

            return new Shopping_List.ShoppingList
            {
                Id = updatedShoppingList.Id,
                Category = updatedShoppingList.Category,
                Name = updatedShoppingList.Name,
                Description = updatedShoppingList.Description,
                IsCompleted = updatedShoppingList.IsCompleted,
                DateTimeAdded = updatedShoppingList.DateTimeAdded.Date.ToShortDateString()
            };
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteShoppingList(string id)
        {
            var existingShoppingList = this.shoppingListsRepository.GetItemAsync(id);
            if (existingShoppingList == null)
            {
                return NotFound($"Could not find shopping list with id = {id}");
            }

            this.shoppingListsRepository.DeleteItemAsync(id);

            return Ok();
        }
    }
}
