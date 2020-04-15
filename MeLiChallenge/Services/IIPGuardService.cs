using MeLiChallenge.Domain;
using System.Threading.Tasks;

namespace MeLiChallenge.Services.Externals
{
    public interface IIPGurardService
    {
        /// <summary>
        /// Obtiene la instancia del pais según ip
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<Country> GetCountry(string ipAddress);

        /// <summary>
        /// Obtiene las estadisticas
        /// </summary>
        /// <returns></returns>
        Task<Statistics> GetStatistics();
    }
}
