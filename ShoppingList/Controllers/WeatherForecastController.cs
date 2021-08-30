using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

            

            var id = Guid.NewGuid();
            var newItem = new Item
            {
                Id = id.ToString(),
                Name = "books",
                Description = "Harry Potter",
                Category = "personal",
                IsCompleted = false
            };
            await _cosmosDbService.AddItemAsync(newItem);

            var testItem = await _cosmosDbService.GetItemAsync("7d368e0a-1003-4d6c-aae7-bd9a22d4d998");
            if (testItem != null)
            {
                await _cosmosDbService.DeleteItemAsync("7d368e0a-1003-4d6c-aae7-bd9a22d4d998");
            }

            newItem.Description = "Harry Potter and the Chamber of Secrets";
            await _cosmosDbService.UpdateItemAsync(id.ToString(), newItem);

            var rng = new Random();
            var items = await _cosmosDbService.GetItemsAsync("SELECT * FROM c");
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
}
