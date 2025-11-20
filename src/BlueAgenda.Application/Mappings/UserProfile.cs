using AutoMapper;
using BlueAgenda.Application.Models;
using BlueAgenda.Domain.Entities;

namespace BlueAgenda.Application.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, CreateUserModel>();
    }
}
