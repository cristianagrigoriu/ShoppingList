using System;
using System.Threading.Tasks;
using Shopping_List.Controllers;
using Amqp;

namespace Shopping_List.Messaging
{
    public class AmqpNotificationService : INotificationService
    {
        public async Task Send(IEvent newShoppingListAddedEvent)
        {
            Address address = new Address("amqp://admin:admin@localhost:5672");
            Connection connection = new Connection(address);
            Session session = new Session(connection);

            Message message = new Message("Hello AMQP!");
            SenderLink sender = new SenderLink(session, "sender-link", "q1");
            sender.Send(message);
            Console.WriteLine("Sent Hello AMQP!");

            sender.Close();
            session.Close();
            connection.Close();
        }
    }
}
