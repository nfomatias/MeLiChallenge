using System;
using System.Threading.Tasks;

namespace MeLiChallenge.Services
{
    public interface ICacheService
    {
        Task<T> GetCacheValueAsync<T>(string key);
        Task SetCacheValueAsync<T>(string key, T value);
        Task SetCacheValueAsync<T>(string key, T value, TimeSpan ttl);
        void Increment(string key);
    }
}
