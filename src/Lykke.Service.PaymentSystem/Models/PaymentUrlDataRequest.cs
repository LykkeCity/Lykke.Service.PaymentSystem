using System;
using Common;
using Lykke.Service.PaymentSystem.Core.Enums;

namespace Lykke.Service.PaymentSystem.Models
{
    public class PaymentUrlDataRequest
    {
        public string ClientId { get; set; }
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
        public DepositOption DepositOption { get; set; }
        public string OkUrl { get; set; }
        public string FailUrl { get; set; }
        public string CancelUrl { get; set; }

        public string GetCountryIso3Code()
        {
            if (string.IsNullOrWhiteSpace(Country))
                return null;

            if (CountryManager.HasIso3(Country))
                return Country;

            if (CountryManager.HasIso2(Country))
                return CountryManager.Iso2ToIso3(Country);

            throw new ArgumentException($"Country code {Country} not found in CountryManager");
        }
    }
}
