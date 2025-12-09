using AutoMapper;
using ProjectHK3.Api.Models.PolicyRequest;
using ProjectHK3.Application.DTOs.PolicyApproval;
using ProjectHK3.Application.DTOs.PolicyRequest;

namespace ProjectHK3.Api.Models
{
    public class PolicyRequestMappingProfile: Profile
    {
        public PolicyRequestMappingProfile()
        {
            CreateMap<CreatePolicyRequestModel, CreatePolicyRequestDto>().ReverseMap();
            CreateMap<UpdatePolicyRequestModel, UpdatePolicyRequestDto>().ReverseMap();
            CreateMap<PolicyRequestListDto, PolicyRequestModel>();
            CreateMap<DetailPolicyRequestDto, PolicyRequestDetailModel>();
            CreateMap<PolicyRequestDocumentDto, PolicyRequestDocumentModel>();
        }
    }
}
