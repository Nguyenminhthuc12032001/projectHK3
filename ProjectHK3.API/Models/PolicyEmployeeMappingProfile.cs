using AutoMapper;
using ProjectHK3.Api.Models.PolicyEmployee;
using ProjectHK3.Application.DTOs.PolicyEmployee;

namespace ProjectHK3.Api.Models
{
    public class PolicyEmployeeMappingProfile : Profile
    {
        public PolicyEmployeeMappingProfile()
        {
            CreateMap<PolicyEmployeeDto, PolicyEmployeeModel>().ReverseMap();
            CreateMap<CreatePolicyEmployeeModel, PolicyEmployeeDto>();
            CreateMap<UpdatePolicyEmployeeModel, PolicyEmployeeDto>();
        }
    }
}
