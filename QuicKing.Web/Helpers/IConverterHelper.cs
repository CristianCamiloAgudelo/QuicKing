using QuicKing.Common.Models;
using QuicKing.Web.Data.Entities;

namespace QuicKing.Web.Helpers
{
    public interface IConverterHelper
    {
        TaxiResponse ToTaxiResponse(TaxiEntity taxiEntity);

        TripResponse ToTripResponse(TripEntity tripEntity);
    }

}
