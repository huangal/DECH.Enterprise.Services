using System;
using AutoMapper;
using DECH.Enterprise.Services.Customers.Controllers.Models;

namespace DECH.Enterprise.Services.Customers.Controllers.Mappers
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            //CreateMap<AccountsQueryRequest, AccountsQuery>()
            //    .ForAllMembers( d => d.)
            //.ReverseMap();


            CreateMap<AccountsQueryRequest, AccountsQuery>()
            .ForMember(dest => dest.AccountDescription, opt => opt.MapFrom(src => src.Name));


        }
    }
}
