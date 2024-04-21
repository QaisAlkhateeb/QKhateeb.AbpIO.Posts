using Abp.ContributorPatternExample.Samples;
using Xunit;

namespace Abp.ContributorPatternExample.EntityFrameworkCore.Applications;

[Collection(ContributorPatternExampleTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<ContributorPatternExampleEntityFrameworkCoreTestModule>
{

}
