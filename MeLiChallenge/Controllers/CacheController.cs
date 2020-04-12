using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeLiChallenge.Models;
using MeLiChallenge.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

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
