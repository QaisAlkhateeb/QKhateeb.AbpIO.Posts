using Abp.ContributorPatternExample.Samples;
using Xunit;

namespace Abp.ContributorPatternExample.EntityFrameworkCore.Domains;

[Collection(ContributorPatternExampleTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<ContributorPatternExampleEntityFrameworkCoreTestModule>
{

}
