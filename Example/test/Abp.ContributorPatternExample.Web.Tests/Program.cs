using Microsoft.AspNetCore.Builder;
using Abp.ContributorPatternExample;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
await builder.RunAbpModuleAsync<ContributorPatternExampleWebTestModule>();

public partial class Program
{
}
