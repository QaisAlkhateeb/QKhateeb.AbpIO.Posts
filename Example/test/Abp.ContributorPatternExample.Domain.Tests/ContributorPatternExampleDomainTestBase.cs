using Volo.Abp.Modularity;

namespace Abp.ContributorPatternExample;

/* Inherit from this class for your domain layer tests. */
public abstract class ContributorPatternExampleDomainTestBase<TStartupModule> : ContributorPatternExampleTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
