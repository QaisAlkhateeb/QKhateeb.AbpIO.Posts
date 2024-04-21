using Abp.ContributorPatternExample.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Abp.ContributorPatternExample.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ContributorPatternExampleEntityFrameworkCoreModule),
    typeof(ContributorPatternExampleApplicationContractsModule)
)]
public class ContributorPatternExampleDbMigratorModule : AbpModule
{
}
