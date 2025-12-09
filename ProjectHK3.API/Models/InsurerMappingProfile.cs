using AutoMapper;
using ProjectHK3.Api.Models.Insurer;
using ProjectHK3.Application.DTOs.Insurer;

namespace ProjectHK3.Api.Models
{
    public class InsurerMappingProfile : Profile
    {
        public InsurerMappingProfile()
        {
            CreateMap<CreateInsurerModel, CreateInsurerRequest>();

            CreateMap<UpdateInsurerModel, CreateInsurerRequest>()
                .ForMember(dest => dest.CreateBy, opt => opt.Ignore());

            CreateMap<UpdateInsurerModel, InsurerDetailDto>();

            CreateMap<InsurerListDto, InsurerModel>();
            CreateMap<InsurerDetailDto, InsurerModel>();
        }
    }
}
