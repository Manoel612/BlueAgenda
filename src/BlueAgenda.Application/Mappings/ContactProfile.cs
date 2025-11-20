using AutoMapper;
using BlueAgenda.Application.Models;
using BlueAgenda.Domain.Entities;

namespace BlueAgenda.Application.Mappings;

public class ContactProfile : Profile
{
    public ContactProfile()
    {
        CreateMap<Contact, CreateContactModel>()
            .ReverseMap();

        CreateMap<Contact, ContactModel>();

        CreateMap<Contact, UpdateContactModel>()
            .ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
