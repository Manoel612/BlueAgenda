using AutoMapper;
using BlueAgenda.Application.Models;
using BlueAgenda.Domain.Entities;

namespace BlueAgenda.Infrastructure.Identity;

public class AspNetUserProfile : Profile
{
    public AspNetUserProfile()
    {
        CreateMap<AspNetUser, User>();
        CreateMap<AspNetUser, CreateUserModel>().ReverseMap();
    }
}
