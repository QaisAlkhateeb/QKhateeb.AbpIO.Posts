using Abp.ContributorPatternExample.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Abp.ContributorPatternExample.Web.Pages;

public abstract class ContributorPatternExamplePageModel : AbpPageModel
{
    protected ContributorPatternExamplePageModel()
    {
        LocalizationResourceType = typeof(ContributorPatternExampleResource);
    }
}
