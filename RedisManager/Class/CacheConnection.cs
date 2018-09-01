namespace RedisManager.Class
{
    using StackExchange.Redis;
    using System;
    using System.Configuration;

    public class CacheConnection
    {
        private static readonly Lazy<ConnectionMultiplexer> _lazyConnection;

        static CacheConnection()
        {
            var connectionString = ConfigurationManager.AppSettings["redisKey"];
            _lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(connectionString));
        }

        public static ConnectionMultiplexer Connection => _lazyConnection.Value;
    }
}
