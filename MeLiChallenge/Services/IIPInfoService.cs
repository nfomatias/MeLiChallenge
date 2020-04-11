using MeLiChallenge.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeLiChallenge.Services
{
    public interface IIPInfoService
    {
        Task<IPInfo> GetIPInfo(string ipAddress);
    }
}
