using System;
using Lykke.Contracts.Payments;
using Lykke.Service.PaymentSystem.Core.Domain;
using Lykke.Service.PaymentSystem.Core.Extensions;
using Lykke.Service.PersonalData.Contract.Models;

namespace Lykke.Service.PaymentSystem.Models
{
    public class PaymentTransactionResponse
    {
        public double Amount { get; set; }
        public string AssetId { get; set; }
        public string WalletId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string DepositOption { get; set; }
        public string OkUrl { get; set; }
        public string FailUrl { get; set; }

        public static PaymentTransactionResponse Create(IPaymentTransaction lastPaymentTransaction, IPersonalData personalData)
        {
            if (lastPaymentTransaction.PaymentSystem != CashInPaymentSystem.CreditVoucher
                && lastPaymentTransaction.PaymentSystem != CashInPaymentSystem.Fxpaygate
                && lastPaymentTransaction.PaymentSystem != CashInPaymentSystem.EasyPaymentGateway)
            {
                throw new ArgumentException("Credit voucher payment system is expect for transactionID:" + lastPaymentTransaction.Id);
            }

            var info = lastPaymentTransaction.GetInfo<OtherPaymentInfo>();

            return new PaymentTransactionResponse
            {
                Address = info.Address,
                Amount = lastPaymentTransaction.Amount,
                AssetId = lastPaymentTransaction.AssetId,
                City = info.City,
                Country = info.Country,
                Phone = personalData.ContactPhone,
                Email = personalData.Email,
                FirstName = info.FirstName,
                LastName = info.LastName,
                Zip = info.Zip
            };
        }

        internal static PaymentTransactionResponse Create(IPersonalData personalData)
        {
            return new PaymentTransactionResponse
            {
                Address = personalData.Address,
                City = personalData.City,
                Phone = personalData.ContactPhone,
                Country = personalData.CountryFromPOA ?? personalData.Country,
                Email = personalData.Email,
                FirstName = personalData.FirstName,
                LastName = personalData.LastName,
                Zip = personalData.Zip
            };
        }
    }
}
