using Microsoft.AspNetCore.Mvc;

namespace Shopping_List.ShoppingList.WebApi.Controllers
{
    [Route("shoppingLists")]
    [ApiController]
    public class ShoppingListItemsController : ControllerBase
    {
        [HttpGet("/{shoppingListId}/items")]
        public void GetAllDetails(int shoppingListId)
        {

        }

        [HttpPost("/{shoppingListId}/items")]
        public void AddDetails(int shoppingListId)
        {

        }

        [HttpPut("/{shoppingListId}/items/{itemId}/")]
        public void MarkAsChecked(int shoppingListId, int itemId, [FromBody] ItemInfo markAsChecked)
        {

        }
    }

    public class ItemInfo
    {
        public bool IsChecked { get; set; }
    }
}
