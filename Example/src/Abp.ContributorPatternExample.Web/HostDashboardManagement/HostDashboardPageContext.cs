using System;
using System.Collections.Generic;
using Volo.Abp.DependencyInjection;

namespace Abp.ContributorPatternExample.Web.HostDashboardManagement
{
    public class HostDashboardPageContext : IServiceProviderAccessor
    {
        public List<DashboardWidget> Widgets { get; }

        public IServiceProvider ServiceProvider { get; private set; }

        public HostDashboardPageContext(IServiceProvider serviceProvider)
        {
            Widgets = new List<DashboardWidget>();
            ServiceProvider = serviceProvider;

        }

        public void AddWidget(Type dashboardWidget)
        {
            Widgets.Add(new DashboardWidget
            {
                WidgetType = dashboardWidget,
            });
        }
    }
}
