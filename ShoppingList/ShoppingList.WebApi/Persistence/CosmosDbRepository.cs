namespace Shopping_List.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Azure.Cosmos;

    public class CosmosDbRepository : IShoppingListsRepository
    {
        private readonly Container _container;

        public CosmosDbRepository(CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task<IEnumerable<ShoppingList>> GetAllItems()
        {
            var query = _container.GetItemQueryIterator<ShoppingList>(new QueryDefinition("SELECT * FROM c"));
            var results = new List<ShoppingList>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
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

        public async Task AddItemAsync(ShoppingList shoppingList)
        {
            await this._container.CreateItemAsync<ShoppingList>(shoppingList, new PartitionKey(shoppingList.Id)); //id
        }

        public async Task UpdateItemAsync(string id, ShoppingList shoppingList)
        {
            await this._container.UpsertItemAsync<ShoppingList>(shoppingList); //id?
        }

        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<ShoppingList>(id, new PartitionKey(id)); //id
        }
    }
}