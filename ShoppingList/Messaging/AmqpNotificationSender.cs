using System;
using System.Threading.Tasks;
using Amqp;
using Shopping_List.Controllers;

namespace Shopping_List.Messaging
{
    public class AmqpNotificationSender : INotificationSender
    {
        public Task Send(IEvent newShoppingListAddedEvent)
        {
            Address address = new Address("amqp://admin:admin@localhost:5672");
            Connection connection = new Connection(address);
            Session session = new Session(connection);

            Message message = new Message($"Newly created shopping list with title {newShoppingListAddedEvent.Description}");
            SenderLink sender = new SenderLink(session, "sender-link", "q1");
            sender.Send(message);
            Console.WriteLine("Sent Hello AMQP!");

            sender.Close();
            session.Close();
            connection.Close();

            return Task.CompletedTask;
        }
    }
}
