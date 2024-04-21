using System.Threading.Tasks;
using Volo.Abp.AuditLogging.Web.Pages.Shared.Components.AverageExecutionDurationPerDayWidget;
using Volo.Abp.AuditLogging.Web.Pages.Shared.Components.ErrorRateWidget;
using Volo.Saas.Host.Pages.Shared.Components.SaasEditionPercentageWidget;
using Volo.Saas.Host.Pages.Shared.Components.SaasLatestTenantsWidget;

namespace Abp.ContributorPatternExample.Web.HostDashboardManagement
{
    public class DefaultHostDashboardPageContributor : IHostDashboardPageContributor
    {
        public async Task ConfigureAsync(HostDashboardPageContext context)
        {
            context.AddWidget(typeof(AuditLoggingErrorRateWidgetViewComponent));
            context.AddWidget(typeof(AuditLoggingAverageExecutionDurationPerDayWidgetViewComponent));
            context.AddWidget(typeof(SaasEditionPercentageWidgetViewComponent));
            context.AddWidget(typeof(SaasLatestTenantsWidgetViewComponent));
        }
    }
}
