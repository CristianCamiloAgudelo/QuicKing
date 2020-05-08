using QuicKing.Common.Models;
using QuicKing.Web.Data.Entities;
using System.Collections.Generic;

namespace QuicKing.Web.Helpers
{
    public interface IConverterHelper
    {
        List<TripResponseWithTaxi> ToTripResponse(List<TripEntity> tripEntities);

        TaxiResponse ToTaxiResponse(TaxiEntity taxiEntity);

        TripResponse ToTripResponse(TripEntity tripEntity);

        UserResponse ToUserResponse(UserEntity user);

        List<UserGroupDetailResponse> ToUserGroupResponse(List<UserGroupDetailEntity> users);

    }

}
