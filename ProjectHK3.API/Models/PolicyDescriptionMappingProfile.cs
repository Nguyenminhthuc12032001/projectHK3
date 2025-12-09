using AutoMapper;
using ProjectHK3.Api.Models.PolicyDescription;
using ProjectHK3.Application.DTOs.PolicyDescription;

namespace ProjectHK3.Api.Models
{
    public class PolicyDescriptionMappingProfile : Profile
    {
        public PolicyDescriptionMappingProfile()
        {
            CreateMap<CreatePolicyDescriptionModel, CreatePolicyDescriptionRequest>();
            CreateMap<PolicyDescriptionDto, PolicyDescriptionModel>().ReverseMap();
            CreateMap<UpdatePolicyDescriptionModel, UpdatePolicyDescriptionRequest>();
        }
    }
}
