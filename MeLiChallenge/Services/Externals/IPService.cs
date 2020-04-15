using MeLiChallenge.Models;
using MeLiChallenge.Utils;
using System.Threading.Tasks;

namespace MeLiChallenge.Services.Externals
{
    public class IPService : IIPService
    {
        public async Task<IPData> GetIpData(string ipaddress)
        {
            string uri = "https://api.ip2country.info/ip";
            var apiClient = new ApiClient();

            return await apiClient.GetAsync<IPData>(uri, ipaddress);

        }
    }
}
