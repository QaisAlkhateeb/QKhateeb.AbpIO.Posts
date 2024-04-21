using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Abp.ContributorPatternExample.Data;

/* This is used if database provider does't define
 * IContributorPatternExampleDbSchemaMigrator implementation.
 */
public class NullContributorPatternExampleDbSchemaMigrator : IContributorPatternExampleDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
