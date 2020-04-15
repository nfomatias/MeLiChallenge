using MeLiChallenge.Models;
using MeLiChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace apiNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {

        private readonly ICacheService _cacheService;

        public CacheController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        //GET: api/Cache/5
        [HttpGet]
        public string Get([FromQuery]string key)
        {
            return _cacheService.GetCacheValueAsync<IPData>(key).Result.CountryName;
        }



        // POST: api/Cache
        [HttpPost]
        public void Post([FromBody] KeyValuePair<string, string> keyValue)
        {
            //_database.StringSet(keyValue.Key, keyValue.Value);
        }
    }
}
