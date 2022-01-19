using System;
using Amqp;
using Shopping_List.Messaging;

namespace Messaging
{
    public class AmqpNotificationReceiver : INotificationReceiver
    {
        public string Receive()
        {
            Address address = new Address("amqp://guest:guest@localhost:5672");
            Connection connection = new Connection(address);
            Session session = new Session(connection);
            ReceiverLink receiver = new ReceiverLink(session, "receiver-link", "q1");

            Console.WriteLine("Receiver connected to broker.");
            Message message = receiver.Receive();
            //Console.WriteLine("Received " + message.Body);
            if (message != null)
            {
                receiver.Accept(message);
            }

            receiver.Close();
            session.Close();
            connection.Close();

            return message?.Body.ToString();
        }
    }
}
