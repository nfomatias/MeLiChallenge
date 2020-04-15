using MeLiChallenge.Domain;
using System.Threading.Tasks;

namespace MeLiChallenge.Services
{
    public interface IStatisticService
    {
        /// <summary>
        /// Notifica el país del request que se ha consultado
        /// </summary>
        /// <param name="country"></param>
        void Notify(Country country);

        /// <summary>
        /// Obtiene las estadísticas
        /// </summary>
        /// <returns></returns>
        Task<Statistics> GetStatistics();
    }
}
