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
            var db = _connectionMultiplexer.GetDatabase();
            var result = await db.StringGetAsync(key);

            return result.IsNull ? default : JsonConvert.DeserializeObject<T>(result.ToString());
        }

        public async Task SetCacheValueAsync<T>(string key, T value)
        {
            var db = _connectionMultiplexer.GetDatabase();
            await db.StringSetAsync(key, JsonConvert.SerializeObject(value));
        }

        public async Task SetCacheValueAsync<T>(string key, T value, TimeSpan ttl)
        {
            var db = _connectionMultiplexer.GetDatabase();
            await db.StringSetAsync(key, JsonConvert.SerializeObject(value), ttl);
        }

        public async void Increment(string key)
        {
            var db = _connectionMultiplexer.GetDatabase();
            await db.StringIncrementAsync(key);
        }
    }
}
