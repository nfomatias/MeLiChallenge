using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace MeLiChallenge.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task<T> GetCacheValueAsync<T>(string key)
        {
            return await Task.Run(() =>
            {
                var db = _connectionMultiplexer.GetDatabase();
                var result = db.StringGet(key);
                return result.IsNull ? default : JsonConvert.DeserializeObject<T>(result.ToString());
            });
        }

        public async Task<bool> SetCacheValueAsync<T>(string key, T value)
        {
            var db = _connectionMultiplexer.GetDatabase();
            return await db.StringSetAsync(key, JsonConvert.SerializeObject(value));
        }

        public async Task<bool> SetCacheValueAsync<T>(string key, T value, TimeSpan ttl)
        {
            var db = _connectionMultiplexer.GetDatabase();
            return await db.StringSetAsync(key, JsonConvert.SerializeObject(value), ttl);
        }

        public async Task<long> IncrementAsync(string key)
        {
            var db = _connectionMultiplexer.GetDatabase();
            return await db.StringIncrementAsync(key);
        }
    }
}
