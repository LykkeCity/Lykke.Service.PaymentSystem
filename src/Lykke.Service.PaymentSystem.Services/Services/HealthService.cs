using System.Collections.Generic;
using Lykke.Service.PaymentSystem.Core.Domain.Health;
using Lykke.Service.PaymentSystem.Core.Services;

namespace Lykke.Service.PaymentSystem.Services.Services
{
    // NOTE: See https://lykkex.atlassian.net/wiki/spaces/LKEWALLET/pages/35755585/Add+your+app+to+Monitoring
    public class HealthService : IHealthService
    {
        public string GetHealthViolationMessage()
        {
            return null;
        }

        public IEnumerable<HealthIssue> GetHealthIssues()
        {
            var issues = new HealthIssuesCollection();
            return issues;
        }
    }
}
