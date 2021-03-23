using Application.Common.Models.UserModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserProperties, UserModel>();
            CreateMap<RegisterModel, UserProperties>();
            CreateMap<UpdateModel, UserProperties>();
        }
    }
}
