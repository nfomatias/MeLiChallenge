using MeLiChallenge.Domain;

namespace MeLiChallenge.Services
{
    /// <summary>
    /// Obtiene la instancia de pais de referencia para cálculo de distancia
    /// </summary>
    public interface IReferenceCountryService
    {
        Country GetReferenceCountry();
    }
}