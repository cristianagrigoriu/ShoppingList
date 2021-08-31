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
        private readonly IShoppingListsRepository _shoppingListsRepository;

        public ShoppingListsController(IShoppingListsRepository shoppingListsRepository)
        {
            _shoppingListsRepository = shoppingListsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Shopping_List.ShoppingList>> Get()
        {
            var items = await _shoppingListsRepository.GetAllItems();

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
    }
}
