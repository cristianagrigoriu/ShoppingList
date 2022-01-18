using System.Threading.Tasks;
using Amqp.Listener;

namespace Shopping_List.Messaging
{
    public interface INotificationService
    {
        Task Send(IEvent newShoppingListAddedEvent);

        void Receive();
    }
}