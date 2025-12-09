using AutoMapper;
using ProjectHK3.Api.Models.Auth;
using ProjectHK3.Application.DTOs.Auth;

namespace ProjectHK3.Api.Models
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<LoginModel, LoginRequest>().ReverseMap();
            CreateMap<RegisterModel, RegisterRequest>().ReverseMap();
            CreateMap<NewAdminModel, NewAdminRequest>().ReverseMap();

            CreateMap<ChangePasswordModel.Admin, ChangePasswordRequest.Admin>().ReverseMap();
            CreateMap<ChangePasswordModel.Employee, ChangePasswordRequest.Employee>().ReverseMap();
            CreateMap<ResetPasswordModel, ResetPasswordRequest>().ReverseMap();

            CreateMap<AuthResponse, AuthResponseModel>();
            CreateMap<AuthValidateResponse, AuthValidateResponseModel>();
            CreateMap<UserInfoDto, UserInfoModel>();
        }
    }
}
