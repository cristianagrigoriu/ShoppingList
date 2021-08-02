﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Shopping_List.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Very cool", "Very freezing", "Very hot"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ICosmosDbService _cosmosDbService;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            ICosmosDbService cosmosDbService)
        {
            _logger = logger;
            _cosmosDbService = cosmosDbService;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var workItem = await this._cosmosDbService.GetItemAsync("1");
            var items = await _cosmosDbService.GetMultipleAsync("SELECT * FROM c");

            var rng = new Random();
            return items.Select(item => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(1),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
                Description = item.Description
            })
            .ToArray();
        }
    }

    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task<Item> GetItemAsync(string category)
        {
            try
            {
                ItemResponse<Item> response = await this._container.ReadItemAsync<Item>("1", new PartitionKey("work"));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<IEnumerable<Item>> GetMultipleAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<Item>(new QueryDefinition(queryString));
            var results = new List<Item>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
    }

    public interface ICosmosDbService
    {
        //Task<IEnumerable<Item>> GetItemsAsync(string query);
        Task<IEnumerable<Item>> GetMultipleAsync(string queryString);
        Task<Item> GetItemAsync(string category);
        //Task AddItemAsync(Item item);
        //Task UpdateItemAsync(string id, Item item);
        //Task DeleteItemAsync(string category);
    }

    public class Item
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "isComplete")]
        public bool Completed { get; set; }

        [JsonProperty(PropertyName = "category")]
        public string Category { get; set; }
    }
}
