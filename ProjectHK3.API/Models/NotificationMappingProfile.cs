using AutoMapper;
using ProjectHK3.Api.Models.Notification;
using ProjectHK3.Application.DTOs.Notification;
using ProjectHK3.Domain.Entities;

public class NotificationMappingProfile : Profile
{
    public NotificationMappingProfile()
    {
        CreateMap<SendNotificationModel, SendNotificationRequest>()
            .ForMember(dest => dest.Type,
                opt => opt.MapFrom(src => Enum.Parse<TypeOfNotificationLog>(src.Type, true)));

        CreateMap<NotificationDto, NotificationModel>()
            .ForMember(dest => dest.Type,
                opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status));
    }
}
