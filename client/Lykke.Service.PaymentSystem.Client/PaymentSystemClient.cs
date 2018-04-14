using System;
using Common.Log;

namespace Lykke.Service.PaymentSystem.Client
{
    public class PaymentSystemClient : IPaymentSystemClient, IDisposable
    {
        private readonly ILog _log;

        public PaymentSystemClient(string serviceUrl, ILog log)
        {
            _log = log;
        }

        public void Dispose()
        {
            //if (_service == null)
            //    return;
            //_service.Dispose();
            //_service = null;
        }
    }
}
