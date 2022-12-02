using AutoMapper;
using ITKT_PROJEKTAS.Entities;
using ITKT_PROJEKTAS.Models;

namespace ITKT_PROJEKTAS.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserEditDTO>();
            CreateMap<UserEditDTO, User>();
            CreateMap<ReservationEditDTO, Reservation>();
            CreateMap<Reservation, ReservationEditDTO>();

            CreateMap<RouteImageDTO, Entities.Route>();
            CreateMap<Entities.Route, RouteImageDTO>();
        }
    }
}
