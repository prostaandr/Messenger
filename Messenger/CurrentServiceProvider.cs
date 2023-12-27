namespace Messenger.Server
{
    public static class CurrentServiceProvider
    {
        private static ServiceProvider Provider;

        public static void SetProvider(IServiceCollection services)
        {
            Provider = services.BuildServiceProvider();
        }

        public static ServiceProvider GetProvider()
        {
            return Provider;
        }
    }
}
