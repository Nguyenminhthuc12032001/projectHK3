using AutoMapper;
using ProjectHK3.Application.DTOs.PolicyApproval;
using ProjectHK3.Api.Models.PolicyApproval;

namespace ProjectHK3.Api.Models
{
    public class PolicyApprovalMappingProfile : Profile
    {
        public PolicyApprovalMappingProfile()
        {
            CreateMap<PolicyApprovalDetailDto, PolicyApprovalDetailModel>();
            CreateMap<PolicyRequestListDto, PolicyRequestListModel>();
        }
    }
}
