using AutoMapper;
using Lykke.Service.PaymentSystem.AzureRepositories.Entities;
using Lykke.Service.PaymentSystem.Core.Domain;

namespace Lykke.Service.PaymentSystem.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<IPaymentTransaction, PaymentTransactionEntity>()
                .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(source => source.Id))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(source => source.Status.ToString()))
                .ForMember(dest => dest.PaymentSystem, opt => opt.MapFrom(source => source.PaymentSystem.ToString()))
                .ForMember(dest => dest.PartitionKey, opt => opt.Ignore())
                .ForMember(dest => dest.RowKey, opt => opt.Ignore())
                .ForMember(dest => dest.Timestamp, opt => opt.Ignore())
                .ForMember(dest => dest.ETag, opt => opt.Ignore())
                ;

            CreateMap<IPaymentTransactionEventLog, PaymentTransactionEventLogEntity>()
                .ForMember(dest => dest.PartitionKey, opt => opt.MapFrom(source => source.PaymentTransactionId))
                .ForMember(dest => dest.RowKey, opt => opt.Ignore())
                .ForMember(dest => dest.Timestamp, opt => opt.Ignore())
                .ForMember(dest => dest.ETag, opt => opt.Ignore())
                ;
        }
    }
}
