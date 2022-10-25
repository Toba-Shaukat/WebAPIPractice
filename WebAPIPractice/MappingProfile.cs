using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace AWebAPIPractice
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<T,T>
            CreateMap<Company, CompanyDto>()
                .ForMember(x => x.FullAddress, options => options.MapFrom(x => string.Join(' ', x.Address, x.Country)));
        }
    }
}
