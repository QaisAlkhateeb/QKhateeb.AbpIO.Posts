using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Abp.ContributorPatternExample.Web;

[Dependency(ReplaceServices = true)]
public class ContributorPatternExampleBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "ContributorPatternExample";
}
