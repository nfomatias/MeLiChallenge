using System.Threading.Tasks;
using MeLiChallenge.Services.Externals;
using Microsoft.AspNetCore.Mvc;

namespace MeLiChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IPInfoController : ControllerBase
    {
        private IIPGurardService _ipGuardService;

        public IPInfoController(IIPGurardService ipGuardService)
        {
            _ipGuardService = ipGuardService;
        }

        // GET: api/IPInfo/5
        [HttpGet("{ipaddress}", Name = "Get")]
        public async Task<IActionResult> Get(string ipaddress)
        {
            var result = await _ipGuardService.GetCountry(ipaddress);
            return Ok(result);
        }
    }
}
