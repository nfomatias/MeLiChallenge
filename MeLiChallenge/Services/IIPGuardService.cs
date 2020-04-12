using MeLiChallenge.Domain;
using MeLiChallenge.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeLiChallenge.Services.Externals
{
    public interface IIPGurardService
    {
        Task<Country> GetCountry(string ipAddress);
    }
}
