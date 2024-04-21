using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Abp.ContributorPatternExample.Web.HostDashboardManagement
{
    public class HostDashboardPageContributorClient : IScopedDependency
    {
        private readonly HostDashboardPageManager _manager;

        public HostDashboardPageContributorClient(HostDashboardPageManager hostDashboardPageManager)
        {
            _manager = hostDashboardPageManager;
        }

        public virtual async Task<IReadOnlyList<DashboardWidget>> GetWidgetsAsync()
        {
            var context = await _manager.ConfigureAsync();

            return context.Widgets.AsReadOnly();
        }
    }
}