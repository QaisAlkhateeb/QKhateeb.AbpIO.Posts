using Volo.Abp.Modularity;

namespace Abp.ContributorPatternExample;

[DependsOn(
    typeof(ContributorPatternExampleDomainModule),
    typeof(ContributorPatternExampleTestBaseModule)
)]
public class ContributorPatternExampleDomainTestModule : AbpModule
{

}
