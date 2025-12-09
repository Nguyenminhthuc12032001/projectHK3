using AutoMapper;
using ProjectHK3.Api.Models.Hospital;
using ProjectHK3.Application.DTOs.Hospital;

public class HospitalMappingProfile : Profile
{
    public HospitalMappingProfile()
    {
        CreateMap<CreateHospitalModel, CreateHospital>()
            .ForMember(dest => dest.RegisteredOn,
                opt => opt.MapFrom(src => DateOnly.Parse(src.RegisteredOn)));

        CreateMap<UpdateHospitalModel, UpdateHospital>();

        CreateMap<DetailHospital, HospitalModel>()
            .ForMember(dest => dest.RegisteredOn,
                opt => opt.MapFrom(src => src.RegisteredOn.ToString("yyyy-MM-dd")));

        CreateMap<ListHospital, HospitalListModel>();
    }
}
