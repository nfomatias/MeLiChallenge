using System.Threading.Tasks;
using MeLiChallenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace MeLiChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IPInfoController : ControllerBase
    {
        private IIPInfoService _ipInfoApp;

        public IPInfoController(IIPInfoService ipInfoApp)
        {
            _ipInfoApp = ipInfoApp;
        }

        // GET: api/IPInfo/5
        [HttpGet("{ipaddress}", Name = "Get")]
        public async Task<IActionResult> Get(string ipaddress)
        {
            var result = await _ipInfoApp.GetIPInfo(ipaddress);
            return Ok(result);
        }
    }
}
