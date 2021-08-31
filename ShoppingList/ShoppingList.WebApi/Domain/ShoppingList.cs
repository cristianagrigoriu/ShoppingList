namespace Shopping_List.Controllers
{
    using Newtonsoft.Json;
    using System;

    public class ShoppingList
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "category")]
        public string Category { get; set; }

        [JsonProperty(PropertyName = "isCompleted")]
        public bool IsCompleted { get; set; }

        [JsonProperty(PropertyName = "dateTimeAdded")]
        public DateTime DateTimeAdded { get; set; }
    }
}