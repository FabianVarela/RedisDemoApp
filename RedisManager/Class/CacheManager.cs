namespace RedisManager.Class
{
    using Newtonsoft.Json;
    using RedisManager.Interface;
    using System;
    using System.Configuration;

    public class CacheManager<T> : ICacheManager<T> where T : class
    {
        public bool Exists(string key) => CacheConnection.Connection.GetDatabase().KeyExists(key);

        public T GetValue(string key)
        {
            var cache = CacheConnection.Connection.GetDatabase();
            var members = cache.SetMembers(key);

            if (members.Length == 0) return default(T);

            var memberResult = JsonConvert.DeserializeObject<T>(members[0].ToString());
            return memberResult;
        }

        public void SetValue(string key, T value)
        {
            if (value == null) return;

            int cacheTime = int.Parse(ConfigurationManager.AppSettings["cacheHoursExpire"].ToString());
            var cache = CacheConnection.Connection.GetDatabase();
            var jsonValue = JsonConvert.SerializeObject(value);

            cache.SetAdd(key, jsonValue);
            cache.KeyExpire(key, TimeSpan.FromMinutes(cacheTime));
        }
    }
}
