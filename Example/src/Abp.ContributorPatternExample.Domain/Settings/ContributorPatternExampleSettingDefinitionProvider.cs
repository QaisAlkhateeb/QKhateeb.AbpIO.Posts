using Volo.Abp.Settings;

namespace Abp.ContributorPatternExample.Settings;

public class ContributorPatternExampleSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(ContributorPatternExampleSettings.MySetting1));
    }
}
