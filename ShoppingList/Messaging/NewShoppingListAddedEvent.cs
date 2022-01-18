namespace Shopping_List.Messaging
{
    public class NewShoppingListAddedEvent : IEvent
    {
        private ShoppingListModel newShoppingList { get; }

        public NewShoppingListAddedEvent(ShoppingListModel newShoppingList)
        {
            this.newShoppingList = newShoppingList;
        }

        public string Description => this.newShoppingList.Description;
    }
}