using MeLiChallenge.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeLiChallenge.Services.Externals
{
    public interface IIPService
    {
        Task<IPData> GetIpData(string ipaddress);
    }
}
