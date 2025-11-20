using AutoMapper;
using BlueAgenda.Application.Models;

namespace BlueAgenda.Infrastructure.Identity;

public class AspNetUserProfile : Profile
{
    public AspNetUserProfile()
    {
        CreateMap<AspNetUser, UserModel>();
        CreateMap<AspNetUser, CreateUserModel>().ReverseMap();
    }
}
