using Volo.Abp.Modularity;

namespace Abp.ContributorPatternExample;

public abstract class ContributorPatternExampleApplicationTestBase<TStartupModule> : ContributorPatternExampleTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
