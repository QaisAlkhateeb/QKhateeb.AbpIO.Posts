using System.Threading.Tasks;

namespace Abp.ContributorPatternExample.Web.HostDashboardManagement
{
    public interface IHostDashboardPageContributor
    {
        Task ConfigureAsync(HostDashboardPageContext context);
    }
}
