using MeLiChallenge.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeLiChallenge.Services
{
    public interface IIPService
    {
        Task<IP> GetIpData(string ipaddress);
    }
}
