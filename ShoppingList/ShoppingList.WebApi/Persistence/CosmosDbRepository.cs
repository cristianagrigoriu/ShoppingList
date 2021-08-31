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
    }
}