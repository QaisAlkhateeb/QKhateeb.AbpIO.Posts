using Abp.ContributorPatternExample.Localization;
using Volo.Abp.Application.Services;

namespace Abp.ContributorPatternExample;

/* Inherit your application services from this class.
 */
public abstract class ContributorPatternExampleAppService : ApplicationService
{
    protected ContributorPatternExampleAppService()
    {
        LocalizationResource = typeof(ContributorPatternExampleResource);
    }
}
