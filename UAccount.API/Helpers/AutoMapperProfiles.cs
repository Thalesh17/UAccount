using AutoMapper;
using ProAgil.API.Dtos;
using UAccount.Domain.Dtos;
using UAccount.Domain.Identity;
using UAccount.Domain.Models;

namespace UAccount.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDTO>().ReverseMap();       
            CreateMap<Conta, ContaDTO>().ReverseMap();
        }
    }
}