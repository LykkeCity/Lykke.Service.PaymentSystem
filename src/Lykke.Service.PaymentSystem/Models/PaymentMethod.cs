﻿namespace Lykke.Service.PaymentSystem.Models
{
    public class PaymentMethod
    {
        public string Name { get; set; }
        public string[] Assets { get; set; }
        public bool Available { get; set; }
    }
}
