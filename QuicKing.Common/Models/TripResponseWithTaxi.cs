using System;
using System.Collections.Generic;
using System.Text;

namespace QuicKing.Common.Models
{
    public class TripResponseWithTaxi : TripResponse
    {
        public TaxiResponse Taxi { get; set; }
    }

}
