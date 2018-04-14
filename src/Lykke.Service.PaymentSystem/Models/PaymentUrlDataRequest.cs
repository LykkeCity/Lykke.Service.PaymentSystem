namespace Lykke.Service.PaymentSystem.Models
{
    public class PaymentUrlDataRequest
    {
        public string ClientPaymentSystem { get; set; }
        public string OrderId { get; set; }
        public string ClientId { get; set; }
        public double Amount { get; set; }
        public string AssetId { get; set; }
        public string WalletId { get; set; }
        public string IsoCountryCode { get; set; }
        public string OtherInfoJson { get; set; }
    }
}
