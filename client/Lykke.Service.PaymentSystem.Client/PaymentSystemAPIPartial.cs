using System;
using System.Net.Http;

namespace Lykke.Service.PaymentSystem.Client.AutorestClient
{
    public partial class PaymentSystemAPI
    {
        public PaymentSystemAPI(Uri baseUri, HttpClient client) : base(client)
        {
            Initialize();

            BaseUri = baseUri ?? throw new ArgumentNullException(nameof(baseUri));
        }
    }
}
