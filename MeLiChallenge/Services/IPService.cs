using MeLiChallenge.Entities;
using MeLiChallenge.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeLiChallenge.Services
{
    public class IPService : IIPService
    {
        public async Task<IP> GetIpData(string ipaddress)
        {

            string uri = "https://api.ip2country.info/ip";

            var apiClient = new ApiClient();

            return await apiClient.GetAsync<IP>(uri, ipaddress);

        }
    }
}
