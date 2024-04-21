using System.Collections.Generic;

namespace Abp.ContributorPatternExample.Web.HostDashboardManagement
{
    public class HostDashboardPageOptions
    {
        public List<IHostDashboardPageContributor> Contributors { get; }

        public HostDashboardPageOptions() 
        {
            Contributors = new List<IHostDashboardPageContributor>();
        }
    }
}
