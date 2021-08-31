namespace Shopping_List.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IShoppingListsRepository
    {
        Task<IEnumerable<ShoppingList>> GetAllItems();

        Task<ShoppingList> GetItemAsync(string id);

        Task AddItemAsync(ShoppingList shoppingList);

        Task UpdateItemAsync(string id, ShoppingList shoppingList);

        Task DeleteItemAsync(string id);
    }
}