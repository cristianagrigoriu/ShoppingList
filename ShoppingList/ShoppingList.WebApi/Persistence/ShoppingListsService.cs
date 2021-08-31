namespace Shopping_List.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Azure.Cosmos;

    public class ShoppingListsService
    {
        private readonly IShoppingListsRepository _shoppingListsRepository;
        private readonly Container _container;

        public ShoppingListsService(
            IShoppingListsRepository shoppingListsRepository
            /*CosmosClient dbClient,
            string databaseName,
            string containerName*/)
        {
            _shoppingListsRepository = shoppingListsRepository;
            //this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task<ShoppingList> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<ShoppingList> response = await this._container.ReadItemAsync<ShoppingList>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<IEnumerable<ShoppingList>> GetItemsAsync()
        {
            return await this._shoppingListsRepository.GetAllItems();
        }

        public async Task AddItemAsync(ShoppingList shoppingList)
        {
            await this._container.CreateItemAsync<ShoppingList>(shoppingList, new PartitionKey(shoppingList.Category));
        }

        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<ShoppingList>(id, new PartitionKey("personal"));
        }

        public async Task UpdateItemAsync(string id, ShoppingList shoppingList)
        {
            await this._container.UpsertItemAsync<ShoppingList>(shoppingList, new PartitionKey("personal"));
        }
    }
}