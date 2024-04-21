using Volo.Abp.Modularity;

namespace Abp.ContributorPatternExample;

[DependsOn(
    typeof(ContributorPatternExampleApplicationModule),
    typeof(ContributorPatternExampleDomainTestModule)
)]
public class ContributorPatternExampleApplicationTestModule : AbpModule
{

}
