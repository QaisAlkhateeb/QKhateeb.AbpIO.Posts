using System.Threading.Tasks;

namespace Abp.ContributorPatternExample.Data;

public interface IContributorPatternExampleDbSchemaMigrator
{
    Task MigrateAsync();
}
