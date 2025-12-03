using AutoMapper;
using ProjectHK3.Api.Models.Auth;
using ProjectHK3.Application.DTOs.Auth;

namespace ProjectHK3.Api.Models
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<LoginModel, LoginRequest>();
            CreateMap<RegisterModel, RegisterRequest>();
            CreateMap<NewAdminModel, NewAdminRequest>();

            CreateMap<ChangePasswordModel.Admin, ChangePasswordRequest.Admin>();
            CreateMap<ChangePasswordModel.Employee, ChangePasswordRequest.Employee>();
            CreateMap<ResetPasswordModel, ResetPasswordRequest>();

            CreateMap<AuthResponse, AuthResponseModel>();
            CreateMap<AuthValidateResponse, AuthValidateResponseModel>();
            CreateMap<UserInfoDto, UserInfoModel>();
        }
    }
}
