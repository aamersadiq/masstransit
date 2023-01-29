namespace Common
{
    public static class AppSettings
    {
        public static string QueueName = "aa-test-queue";
        // sb cs: Endpoint=sb://sb-sms-dev-sfjfil.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=cLs+XS7p8RxGC5G3+ZqDRKw7VPSLF/9Kmm/tz7QGwkY=
        // rmq cs: amqp://guest:guest@localhost:5672
        public static string ServiceBusConnectionString = "Endpoint=sb://sb-sms-dev-sfjfil.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=cLs+XS7p8RxGC5G3+ZqDRKw7VPSLF/9Kmm/tz7QGwkY=";
    }
}
