using AutoMapper;
using Lykke.Service.PaymentSystem.Core.Domain;
using Lykke.Service.PaymentSystem.Models;

namespace Lykke.Service.PaymentSystem
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<IPaymentTransaction, PaymentTransactionResponse>()
                .ForMember(dest => dest.OtherData, opt => opt.Ignore())
                ;
        }
    }
}
