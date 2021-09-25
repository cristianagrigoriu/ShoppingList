namespace Shopping_List.Controllers
{
    public class UpdateShoppingListModel
    {
        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}