using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Lykke.Service.PaymentSystem.Core.Services;

namespace Lykke.Service.PaymentSystem.Services
{
    // NOTE: Sometimes, shutdown process should be expressed explicitly. 
    // If this is your case, use this class to manage shutdown.
    // For example, sometimes some state should be saved only after all incoming message processing and 
    // all periodical handler was stopped, and so on.
    
    public class ShutdownManager : IShutdownManager
    {
        private readonly List<IStopable> _items = new List<IStopable>();

        public void Register(IStopable stoppable)
        {
            _items.Add(stoppable);
        }

        public async Task StopAsync()
        {
            foreach (var item in _items)
            {
                item.Stop();
            }

            await Task.CompletedTask;
        }
    }
}
