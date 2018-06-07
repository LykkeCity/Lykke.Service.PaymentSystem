using System;

namespace Lykke.Service.PaymentSystem.Core.Domain
{
    public interface IAppGlobalSettings
    {
        string DepositUrl { get; set; }
        bool DebugMode { get; set; }
        string DefaultIosAssetGroup { get; set; }
        string DefaultAssetGroupForOther { get; set; }
        bool IsOnReview { get; set; }
        double? MinVersionOnReview { get; set; }
        double IcoLkkSold { get; set; }
        bool IsOnMaintenance { get; set; }
        int LowCashOutTimeoutMins { get; set; }
        int LowCashOutLimit { get; set; }
        bool MarginTradingEnabled { get; set; }
        bool CashOutBlocked { get; set; }
        bool BtcOperationsDisabled { get; set; }
        bool BitcoinBlockchainOperationsDisabled { get; set; }
        bool LimitOrdersEnabled { get; set; }
        double MarketOrderPriceDeviation { get; set; }
        string[] BlockedAssetPairs { get; set; }
        string OnReviewAssetConditionLayer { get; set; }
        DateTime? IcoStartDtForWhitelisted { get; set; }
        DateTime? IcoStartDt { get; set; }
        bool ShowIcoBanner { get; set; }
    }
}
