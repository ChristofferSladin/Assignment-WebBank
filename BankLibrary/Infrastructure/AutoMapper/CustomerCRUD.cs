using Assignment_WebBank.BankAppData;
using AutoMapper;
using BankLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.Infrastructure.AutoMapper
{
    public class CustomerCRUD : Profile
    {
        public CustomerCRUD()
        {
            // Källa => Mål
            // CreateEmployeeViewModel => Employee
            CreateMap<ValidateCustomerVM, Customer>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Givenname, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Streetaddress, opt => opt.MapFrom(src => src.Adress))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.Zipcode, opt => opt.MapFrom(src => src.ZipCode))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.BirthDay))
                .ForMember(dest => dest.NationalId, opt => opt.MapFrom(src => src.PersonalNr))
                .ForMember(dest => dest.Telephonenumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Emailaddress, opt => opt.MapFrom(src => src.Email))
                .ReverseMap()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Givenname))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Surname))
                .ForMember(dest => dest.Adress, opt => opt.MapFrom(src => src.Streetaddress))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.Zipcode))
                .ForMember(dest => dest.BirthDay, opt => opt.MapFrom(src => src.Birthday))
                .ForMember(dest => dest.PersonalNr, opt => opt.MapFrom(src => src.NationalId))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Telephonenumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Emailaddress));
        }
    }
}
