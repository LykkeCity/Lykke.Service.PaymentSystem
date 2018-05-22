using System.Threading.Tasks;
using Lykke.Service.PaymentSystem.Core.Services;
using Lykke.Service.PaymentSystem.Core.Settings.ServiceSettings.PaymentSystem;
using Lykke.Service.PaymentSystem.Services.Services;
using MarginTrading.Backend.Contracts.Account;
using MarginTrading.Backend.Contracts.DataReaderClient;
using Xunit;
using Moq;

namespace Lykke.Service.PaymentSystem.Tests.Services
{
    public class LegalEntityServiceTest
    {

        private readonly string _legalEntity;

        public LegalEntityServiceTest()
        {
            _legalEntity = "TestLegalEntity";
        }

        [Fact]
        public async Task Get_LegalEntity_WalletId_IsEmpty()
        {
            var walletId = string.Empty;

            var mtDataReaderClientMock = new Mock<IMtDataReaderClient>();
            mtDataReaderClientMock.Setup(foo => foo.AccountsApi.GetAccountById(walletId))
                .ReturnsAsync(() => new DataReaderAccountBackendContract());

            var mtDataReaderClientsPairMock = new Mock<IMtDataReaderClientsPair>();
            mtDataReaderClientsPairMock.Setup(foo => foo.Get(true))
                .Returns(() => mtDataReaderClientMock.Object);


            var paymentSettings = new PaymentSettings
            {
                LegalEntity = _legalEntity
            };

            var legalEntityService = new LegalEntityService(mtDataReaderClientsPairMock.Object, paymentSettings);

            
            Assert.True(await legalEntityService.GetLegalEntityAsync(walletId) == _legalEntity);
        }
    }

}
