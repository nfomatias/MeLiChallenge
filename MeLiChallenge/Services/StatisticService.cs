using MeLiChallenge.Domain;
using MeLiChallenge.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeLiChallenge.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly ICacheService _cacheService;
        private static readonly string _farthestCountryKey = "FartestKey";
        private static readonly string _nearestCountryKey = "NearestKey";

        /// <summary>
        /// Distancia promedio sin división de la cantidad de requests
        /// </summary>
        private static readonly string _partialAverageDistanceKey = "partialAverage";

        /// <summary>
        /// Contador de requests globales
        /// </summary>
        private static readonly string _requestsCountKey = "RequestsCount";

        public StatisticService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<Statistics> GetStatistics()
        {
            var farthestCountryCode = _cacheService.GetCacheValueAsync<KeyValuePair<string, int>>(_farthestCountryKey).Result.Key;
            var nearestCountryCode = _cacheService.GetCacheValueAsync<KeyValuePair<string, int>>(_nearestCountryKey).Result.Key;

            var farthestCountryData = await _cacheService.GetCacheValueAsync<CountryData>(farthestCountryCode);
            var nearestCountryData = await _cacheService.GetCacheValueAsync<CountryData>(nearestCountryCode);
            var requestsCount = await _cacheService.GetCacheValueAsync<int>(_requestsCountKey);
            var partialAverage = await _cacheService.GetCacheValueAsync<int>(_partialAverageDistanceKey);

            var avg = Convert.ToUInt32(partialAverage / requestsCount);

            return new Statistics(new Country(farthestCountryData), new Country(nearestCountryData), avg);
        }


        delegate bool CompareDelegate(int x, int y);

        public void Notify(Country country)
        {
            CalculateNewAverage(country);

            CompareDelegate lessOrEqualThan = delegate (int x, int y) { return x <= y; };
            CompareDelegate moreOrEqualThan = delegate (int x, int y) { return x >= y; };

            ProcessCountryDistance(country, lessOrEqualThan, _nearestCountryKey);
            ProcessCountryDistance(country, moreOrEqualThan, _farthestCountryKey);

            _cacheService.Increment(_requestsCountKey);
        }

        /// <summary>
        /// Calcula nueva distancaia máxima/mínima y guarda el resultado en cache
        /// </summary>
        /// <param name="country"></param>
        /// <param name="compare"></param>
        /// <param name="key"></param>
        private void ProcessCountryDistance(Country country, CompareDelegate compare, string key)
        {
            var countryKVP = _cacheService.GetCacheValueAsync<KeyValuePair<string, int>>(key).Result;

            //No existe valor en Cache, inicializo
            if (countryKVP.Equals(default(KeyValuePair<string, int>)))
            {
                countryKVP = new KeyValuePair<string, int>(country.Code, country.ReferenceDistance);
                _cacheService.SetCacheValueAsync(key, countryKVP);
                return;
            }

            //nuevo máximo o mínimo
            if (country.Code != countryKVP.Key && compare(country.ReferenceDistance, countryKVP.Value))
            {
                countryKVP = new KeyValuePair<string, int>(country.Code, country.ReferenceDistance);
                _cacheService.SetCacheValueAsync(key, countryKVP);
                return;
            }
        }

        /// <summary>
        /// Calcula y mantiene el estado de la distancia promedio de todas las ejecuciones
        /// </summary>
        /// <param name="country"></param>
        private void CalculateNewAverage(Country country)
        {
            /*
             * Ejemplo del ejercicio:(2862 km * 10 + 10040 km* 5) / 15 = 5254 km
             */
            var partialAverage = _cacheService.GetCacheValueAsync<ulong>(_partialAverageDistanceKey).Result;

            partialAverage += Convert.ToUInt64(country.ReferenceDistance);

            _cacheService.SetCacheValueAsync(_partialAverageDistanceKey, partialAverage);
        }
    }
}
