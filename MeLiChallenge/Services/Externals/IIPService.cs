using MeLiChallenge.Models;
using System.Threading.Tasks;

namespace MeLiChallenge.Services.Externals
{
    public interface IIPService
    {
        Task<IPData> GetIpData(string ipaddress);
    }
}
