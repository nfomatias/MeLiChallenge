using MeLiChallenge.Models;
using System.Threading.Tasks;

namespace MeLiChallenge.Services.Externals
{
    public interface IExchangeService
    {
        Task<ExchangeData> GetExchangeData(string currencyCode);
    }
}