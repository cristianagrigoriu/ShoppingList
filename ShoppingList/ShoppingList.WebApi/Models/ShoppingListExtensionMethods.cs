namespace Shopping_List.ShoppingList.WebApi.Models
{
    public static class ShoppingListExtensionMethods
    {
        public static ShoppingListModel ToModel(this Shopping_List.Controllers.ShoppingList item)
        {
            return new ShoppingListModel
            {
                Id = item.Id,
                Category = item.Category,
                Name = item.Name,
                Description = item.Description,
                IsCompleted = item.IsCompleted,
                CreationDate = item.DateTimeAdded
            };
        }
    }
}
