using AutoMapper;
using ProjectHK3.Api.Models.Finance;
using ProjectHK3.Application.DTOs.Finance;
using ProjectHK3.Domain.Entities;

public class FinanceMappingProfile : Profile
{
    public FinanceMappingProfile()
    {
        // Create
        CreateMap<CreateTransactionModel, TransactionLedger>()
            .ForMember(dest => dest.Method,
                opt => opt.MapFrom(src => Enum.Parse<MethodOfTransactionLedger>(src.Method, true)))
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => StatusOfTransactionLedger.Pending));

        // Update status
        CreateMap<UpdateTransactionStatusModel, TransactionLedger>()
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => Enum.Parse<StatusOfTransactionLedger>(src.Status, true)));

        // DTO to API Models
        CreateMap<DisbursementDetailDto, DisbursementDetailModel>();
        CreateMap<DisbursementListDto, DisbursementListModel>();
        CreateMap<TransactionHistoryDto, TransactionHistoryModel>();
    }
}
