using System;
using Shopping_List.Controllers;

namespace Shopping_List
{
    public class ShoppingListModel
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreationDate { get; set; } //maybe datetime?
    }
}