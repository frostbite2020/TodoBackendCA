using Application.Common.Models.UserModels;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserProperty, UserModel>();
            CreateMap<UpdateModel, UserProperty>();
        }
    }
}
