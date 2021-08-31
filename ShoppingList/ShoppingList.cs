﻿namespace Shopping_List
{
    public class ShoppingList
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public string DateTimeAdded { get; set; } //maybe datetime?
    }
}
