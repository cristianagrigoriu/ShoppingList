namespace Shopping_List.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IShoppingListsRepository
    {
        Task<IEnumerable<ShoppingList>> GetAllItems();
    }
}