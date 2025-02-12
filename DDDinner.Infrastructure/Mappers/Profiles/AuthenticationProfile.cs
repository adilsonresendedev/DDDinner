using AutoMapper;
using DDDinner.Application.Authentication.Commands.Register;
using DDDinner.Application.Authentication.Queries;
using DDDinner.Contracts.Authentication;

namespace DDDinner.Infrastructure.Mappers.Profiles
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<RegisterRequest, RegisterCommand>().ReverseMap();
            CreateMap<LoginRequest, LoginQuery>().ReverseMap();
        }
    }
}
