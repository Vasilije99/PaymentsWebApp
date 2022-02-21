using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Payments.Dtos;
using WebAPI_Payments.Models;

namespace WebAPI_Payments.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<TransactionsHistory, TransactionsDto>().ReverseMap();
        }
    }
}
