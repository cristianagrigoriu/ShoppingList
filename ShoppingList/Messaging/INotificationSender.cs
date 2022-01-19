using System.Threading.Tasks;
using Amqp.Listener;

namespace Shopping_List.Messaging
{
    public interface INotificationSender
    {
        Task Send(IEvent newShoppingListAddedEvent);
    }
}