using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Abp.ContributorPatternExample.Web.HostDashboardManagement
{
    public class HostDashboardPageManager : IScopedDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public HostDashboardPageManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public virtual async Task<HostDashboardPageContext> ConfigureAsync() 
        {
            var context = new HostDashboardPageContext(_serviceProvider);
            var contributors = _serviceProvider.GetRequiredService<IOptions<HostDashboardPageOptions>>().Value.Contributors;

            foreach (var contributor in contributors)
            {
                await contributor.ConfigureAsync(context);
            }

            return context;
        }

        public virtual async Task<IReadOnlyList<DashboardWidget>> GetWidgetsAsync()
        {
            var context = await ConfigureAsync();

            return context.Widgets.AsReadOnly();
        }
    }
}
