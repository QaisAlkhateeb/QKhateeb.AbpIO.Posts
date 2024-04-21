using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Abp.ContributorPatternExample.Pages;

[Collection(ContributorPatternExampleTestConsts.CollectionDefinitionName)]
public class Index_Tests : ContributorPatternExampleWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
