namespace Shopping_List.Messaging
{
    public class LogInFileService : ILogService
    {
        private readonly INotificationService notificationService;

        public LogInFileService(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        public void CreateLog()
        {
            this.notificationService.Receive();

        }
    }
}