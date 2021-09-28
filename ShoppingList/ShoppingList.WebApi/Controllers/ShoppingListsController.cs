using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Shopping_List.ShoppingList.WebApi.Models;

namespace Shopping_List.Controllers
{
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
        public async Task<ActionResult<IEnumerable<ShoppingListModel>>> Get()
        {
            var items = await shoppingListsRepository.GetAllItems();

            return Ok(items.Select(item => item.ToModel()));
        }

        [HttpGet("{shoppingListId}")]
        public async Task<ActionResult<ShoppingListModel>> Get([FromRoute] string shoppingListId)
        {
            var item = await shoppingListsRepository.GetItemAsync(shoppingListId);

            if (item == null) return NotFound();

            return Ok(item.ToModel());
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingListModel>> AddShoppingList(CreateShoppingListModel shoppingList)
        {
            var newShoppingList = new ShoppingList
            {
                Category = shoppingList.Category,
                Name = shoppingList.Name,
                Description = shoppingList.Description
            };

            await shoppingListsRepository.AddItemAsync(newShoppingList);

            return Created("", newShoppingList);
        }

        [HttpPut("{id}")]
        public ActionResult<ShoppingListModel> UpdateShoppingList([FromRoute] string id,
            UpdateShoppingListModel updatedShoppingListModel)
        {
            var existingShoppingList = shoppingListsRepository.GetItemAsync(id);
            if (existingShoppingList == null) return NotFound($"Could not find shopping list with id = {id}");

            var updatedShoppingList = new ShoppingList
            {
                Id = id,
                Category = updatedShoppingListModel.Category,
                Name = updatedShoppingListModel.Name,
                Description = updatedShoppingListModel.Description,
                IsCompleted = updatedShoppingListModel.IsCompleted
            };

            shoppingListsRepository.UpdateItemAsync(id, updatedShoppingList);

            return Ok(updatedShoppingListModel); //should return Id in updated list as well?
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteShoppingList(string id)
        {
            var existingShoppingList = shoppingListsRepository.GetItemAsync(id);
            if (existingShoppingList == null) return NotFound($"Could not find shopping list with id = {id}");

            shoppingListsRepository.DeleteItemAsync(id);

            return Ok();
        }
    }
}