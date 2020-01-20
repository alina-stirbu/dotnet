using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelApp.Data;
using HotelApp.Model;

namespace HotelApp.API.Helpers
{
    using AutoMapper;
    using Data.Entities;
    using Model.Hotel;
    using Model.Rate;
    using Model.Room;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<HotelApp.Data.Entities.Hotel, HotelApp.Model.Hotel.HotelResource>();
            CreateMap<CreateHotelResource, Hotel>();

            CreateMap<Room, RoomResource>();
            CreateMap<CreateRoomResource, Room>();

            CreateMap<Rate, RateResource>();
            CreateMap<CreateRateResource, Rate>();
        }
    }
}
