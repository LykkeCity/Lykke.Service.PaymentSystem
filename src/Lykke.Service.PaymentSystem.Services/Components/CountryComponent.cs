using System;
using Common;
using Lykke.Service.PaymentSystem.Core.Components;

namespace Lykke.Service.PaymentSystem.Services.Components
{
    public class CountryComponent: ICountryComponent, IComponent
    {
        public string GetCountryIso3Code(string country)
        {
            if (string.IsNullOrWhiteSpace(country))
                return null;

            if (CountryManager.HasIso3(country))
                return country;

            if (CountryManager.HasIso2(country))
                return CountryManager.Iso2ToIso3(country);

            throw new ArgumentException($"Country code {country} not found in CountryManager");
        }
    }
}
