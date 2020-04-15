using MeLiChallenge.Services.Externals;
using Microsoft.AspNetCore.Mvc;

namespace MeLiChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IPInfoController : ControllerBase
    {
        private readonly IIPGurardService _ipGuardService;

        public IPInfoController(IIPGurardService ipGuardService)
        {
            _ipGuardService = ipGuardService;
        }

        // GET: api/IPInfo/5
        [HttpGet("{ipaddress}", Name = "Get")]
        public IActionResult Get(string ipaddress)
        {
            return Ok(_ipGuardService.GetCountry(ipaddress));
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_ipGuardService.GetStatistics());
        }
    }
}
