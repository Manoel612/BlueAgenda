using AutoMapper;
using BlueAgenda.Application.Models;

namespace BlueAgenda.Application.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserModel, CreateUserModel>();
    }
}
