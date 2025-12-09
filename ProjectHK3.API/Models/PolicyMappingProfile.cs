using AutoMapper;
using ProjectHK3.Application.DTOs.Policy;
using ProjectHK3.Api.Models.Policy;

namespace ProjectHK3.Api.Models
{
    public class PolicyMappingProfile : Profile
    {
        public PolicyMappingProfile()
        {
            CreateMap<CreatePolicyModel, CreatePolicyDto>().ReverseMap();
            CreateMap<UpdatePolicyModel, UpdatePolicyDto>().ReverseMap();

            CreateMap<ListPolicyDto, PolicyModel>();
            CreateMap<DetailPolicyDto, PolicyDetailModel>();
        }
    }
}
