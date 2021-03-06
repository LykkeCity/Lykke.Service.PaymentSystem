﻿using Lykke.Service.ClientAccount.Client;
using Lykke.Service.PaymentSystem.Core.Settings.ServiceSettings.PaymentSystem;
using Lykke.Service.PersonalData.Settings;

namespace Lykke.Service.PaymentSystem.Core.Settings.ServiceSettings
{
    public class PaymentSystemSettings
    {
        public DbSettings Db { get; set; }
        public PaymentSettings PaymentSettings { get; set; }
        public MarginSettings MarginSettings { get; set; }
    }
}
