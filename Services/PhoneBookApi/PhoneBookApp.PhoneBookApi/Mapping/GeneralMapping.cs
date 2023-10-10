using AutoMapper;
using PhoneBookApp.PhoneBookApi.Dtos;
using PhoneBookApp.PhoneBookApi.Models;

namespace PhoneBookApp.PhoneBookApi.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Contact, ContactDto>().ReverseMap();
            CreateMap<ContactInfo, ContactInfoDto>().ReverseMap();

            CreateMap<ContactInfo, ContactInfoCreateDto>().ReverseMap();
            CreateMap<Contact, ContactCreateDto>().ReverseMap();           
          
        }
    }
}
