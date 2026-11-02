using AutoMapper;
using SmartBite.CustomerProfile.Application.Dtos;
using SmartBite.CustomerProfile.Domain.Entities;

namespace SmartBite.CustomerProfile.Application.Mappers
{
    public class CustomerProfile:Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerInfoDto, CustomerInfo>().ReverseMap();
        }
    }
}
