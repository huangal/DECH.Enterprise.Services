using AutoMapper;
using DECH.Enterprise.Services.Customers.Contracts.Models;

namespace DECH.Enterprise.Services.Customers.Services.Mappers
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerModel>()
            .ReverseMap();
        }
    }
}
