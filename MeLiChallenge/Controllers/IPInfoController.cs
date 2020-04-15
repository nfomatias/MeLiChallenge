using MeLiChallenge.Services.Externals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MeLiChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IPInfoController : ControllerBase
    {
        private readonly IIPGurardService _ipGuardService;
        private readonly ILogger _logger;

        public IPInfoController(IIPGurardService ipGuardService, ILogger<IPInfoController> logger)
        {
            _ipGuardService = ipGuardService;
            _logger = logger;
        }

        // GET: api/IPInfo/5
        [HttpGet("{ipaddress}", Name = "Get")]
        public IActionResult Get(string ipaddress)
        {
            try
            {
                return Ok(_ipGuardService.GetCountry(ipaddress));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_ipGuardService.GetStatistics());
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
