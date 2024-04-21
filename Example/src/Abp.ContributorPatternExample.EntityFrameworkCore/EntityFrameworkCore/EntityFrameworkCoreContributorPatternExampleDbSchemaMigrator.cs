using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Abp.ContributorPatternExample.Data;
using Volo.Abp.DependencyInjection;

namespace Abp.ContributorPatternExample.EntityFrameworkCore;

public class EntityFrameworkCoreContributorPatternExampleDbSchemaMigrator
    : IContributorPatternExampleDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreContributorPatternExampleDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the ContributorPatternExampleDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<ContributorPatternExampleDbContext>()
            .Database
            .MigrateAsync();
    }
}
