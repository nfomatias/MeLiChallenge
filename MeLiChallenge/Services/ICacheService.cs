using System;
using System.Threading.Tasks;

namespace MeLiChallenge.Services
{
    public interface ICacheService
    {
        /// <summary>
        /// Obtiene una instancia de un objeto a partir de una key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> GetCacheValueAsync<T>(string key);

        /// <summary>
        /// Guarda una instancia
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task SetCacheValueAsync<T>(string key, T value);

        /// <summary>
        /// Guarda una instancia con un tiempo de vida determinado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="ttl"></param>
        /// <returns></returns>
        Task SetCacheValueAsync<T>(string key, T value, TimeSpan ttl);

        /// <summary>
        /// Incrementa un counter
        /// </summary>
        /// <param name="key"></param>
        void Increment(string key);
    }
}
