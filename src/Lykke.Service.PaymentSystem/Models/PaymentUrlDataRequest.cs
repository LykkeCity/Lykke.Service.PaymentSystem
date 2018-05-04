using System;
using Common;
using Lykke.Service.PaymentSystem.Core.Enums;
using Newtonsoft.Json;

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
        public string DepositOption { get; set; }
        public string OkUrl { get; set; }
        public string FailUrl { get; set; }

        [JsonIgnore]
        public DepositOption DepositOptionEnum
        {
            get
            {
                if (string.IsNullOrWhiteSpace(DepositOption))
                    return Core.Enums.DepositOption.BankCard;

                return Enum.TryParse(DepositOption, out DepositOption tmpOption)
                    ? tmpOption
                    : Core.Enums.DepositOption.BankCard;
            }
        }

        public string GetCountryIso3Code()
        {
            if (string.IsNullOrWhiteSpace(Country))
                return null;

            if (CountryManager.HasIso3(Country))
                return Country;

            if (CountryManager.HasIso2(Country))
                return CountryManager.Iso2ToIso3(Country);

            throw new Exception($"Country code {Country} not found in CountryManager");
        }
    }
}
