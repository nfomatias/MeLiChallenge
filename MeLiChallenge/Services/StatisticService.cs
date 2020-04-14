using MeLiChallenge.Domain;
using MeLiChallenge.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeLiChallenge.Services
{
    public class StatisticService : IStatisticService
    {
        private ICacheService _cacheService;
        private static string _farthestCountryKey = "FartestKey";
        private static string _nearestCountryKey = "NearestKey";

        /// <summary>
        /// Distancia promedio sin división de la cantidad de requests
        /// </summary>
        private static string _partialAverageDistanceKey = "partialAverage";
        /// <summary>
        /// Contador de requests por pais. Ej: CountUS (contador USA), CountDE (contador alemania)
        /// </summary>
        private static string _countryCountKey = "Count";

        /// <summary>
        /// Contador de requests globales
        /// </summary>
        private static string _requestsCountKey = "RequestsCount";

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

        private void CalculateNewAverage(Country country)
        {
            var countryCount = _cacheService.GetCacheValueAsync<ulong>(country.Code + _countryCountKey).Result;
            var partialAverage = _cacheService.GetCacheValueAsync<ulong>(_partialAverageDistanceKey).Result;

            var referenceDistance = Convert.ToUInt64(country.ReferenceDistance);

            if (countryCount == 0)
            {
                //Si es la 1ra vez, solo sumo la distancia
                partialAverage += referenceDistance;
            }
            else
            {
                //si es la n vez, debo restar del average el valor anterior
                partialAverage -= referenceDistance * countryCount;

                //y sumo el nuevo valor
                partialAverage += referenceDistance * (countryCount + 1);
            }

            _cacheService.SetCacheValueAsync<ulong>(country.Code + _countryCountKey, countryCount + 1);
            _cacheService.SetCacheValueAsync<ulong>(_partialAverageDistanceKey, partialAverage);
        }
    }
}
