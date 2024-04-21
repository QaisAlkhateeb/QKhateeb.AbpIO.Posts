using Abp.ContributorPatternExample.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Abp.ContributorPatternExample.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class ContributorPatternExampleController : AbpControllerBase
{
    protected ContributorPatternExampleController()
    {
        LocalizationResource = typeof(ContributorPatternExampleResource);
    }
}
