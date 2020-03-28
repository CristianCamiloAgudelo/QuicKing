using QuicKing.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuicKing.Common.Services
{
    public interface IApiService
    {
        Task<Response> GetTaxiAsync(string plaque, string urlBase, string servicePrefix, string controller);
    }

}
