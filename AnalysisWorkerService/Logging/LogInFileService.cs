using System;

namespace Shopping_List.Messaging
{
    public class LogInFileService : ILogService
    {
        private readonly INotificationReceiver notificationReceiver;

        public LogInFileService(INotificationReceiver notificationSender)
        {
            this.notificationReceiver = notificationSender;
        }

        public void ReceiveLog()
        {
            var message = this.notificationReceiver.Receive();
            Console.WriteLine(message);
        }
    }
}