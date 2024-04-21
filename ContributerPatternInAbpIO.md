# Introduction

Contributers pattern have beed used in multiple modules and packages of Abp framework (Toolbar Contributor, Tenant Resolver, Settings Page Contributer, Image Resizer Contributor)

Contibutor pattern allow external modules to contributes on execute/render components in existing modules, the most common example is `IMenuContributor` [Navigation-Menu](https://docs.abp.io/en/abp/latest/UI/AspNetCore/Navigation-Menu).

The `AbpUiNavigationModule` allow external modules to contributes and add their navigation menus. this makes the UI Navigation centralized on the applicaton, open for extension and closed for modificatin   

```cs
using System.Threading.Tasks;
using MyProject.Localization;
using Volo.Abp.UI.Navigation;

namespace MyProject.Web.Menus
{
    public class MyProjectMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            var l = context.GetLocalizer<MyProjectResource>();

            context.Menu.AddItem(
                new ApplicationMenuItem("MyProject.Crm", l["Menu:CRM"])
                    .AddItem(new ApplicationMenuItem(
                        name: "MyProject.Crm.Customers", 
                        displayName: l["Menu:Customers"], 
                        url: "/crm/customers")
                    ).AddItem(new ApplicationMenuItem(
                        name: "MyProject.Crm.Orders", 
                        displayName: l["Menu:Orders"],
                        url: "/crm/orders")
                     )
            );
        }
    }
}
```
# Deep dive in Abp.io Contributer pattern:

## Definition
The contributor pattern is used to allow various parts of an application to contribute or inject additional functionality into a core component without directly modifying it. in simple words it's an implementation for OC princeple.

    Software entities (classes, modules, functions, etc.) should be open for extension, but closed for modification.


## Analysis

let's start by analysing two of the most common usage of `Contributer Pattern`

#### 1) Menu conitrubutor:

![MenuClassDiagram](menu_code.png)

As shown in the diagram, menu *contributors* that implement `IMenuContributo` can manage and configgure thier menu items by accessing `MenuConfigurationContext`, `MenuConfigurationContext` shared between all contributors and represents the *data model*.

the implementation of `IMenuManager` (*manager*) creates `MenuConfigurationContext`, load all contibutors and execute `ConfigureMenuAsync`.


#### 2) Settings Page Contributor:

![SettingsClassDiagram](settings_code.png)

I think it's now more clear and you can start capture the pattern, in settings page again we have the `SettingPageCreationContext` which shared between all contributors and contains the settings groups (*data model*).

the `SettingPageContributorManager` creates the `SettingPageCreationContext`, loads the contributors by the help of DI `SettingManagementPageOptions`, and execute the `contributor.ConfigureAsync(context);`

A slight different between both MenuContributor strucutre and SettingsPageContributor structor, that `SettingPageCreationContext` exposed to consumers while in Menu management you can access the data model only by using `IMenuManager`

## Structure of Contributor Pattern:


* *ComponentContext*: a shared context with all contributors contains initial values and data models related to contributor component.

* *Contributor*: Implements `Configure` method that acccess `ComponentContext` to add it's contribution to a component.

* *ContributorManager*: Create and initialize `ComponentContext`, load and configure `Contributor`s

* *ContributorOptions*: a useage of `IOptions<>` pattern to use DI to load contributors, used by `ContributorManager`


![ContributorPatternClassDiagram](code.png)

# Example:

Let's apply this pattern in new example. when you create new Abp application you will find `HostDashboard` Page:

```html
<!-- HostDashboard.cshtml -->

<div id="HostDashboardWidgetsArea" data-widget-filter="#DashboardFilterForm">
    <abp-row>
        
        @if (await WidgetManager.IsGrantedAsync(typeof(AuditLoggingErrorRateWidgetViewComponent)))
        {
            <abp-column size-md="_12" size-lg="_6">
                @await Component.InvokeAsync(typeof(AuditLoggingErrorRateWidgetViewComponent))
            </abp-column>
        }
        @if (await WidgetManager.IsGrantedAsync(typeof(AuditLoggingAverageExecutionDurationPerDayWidgetViewComponent)))
        {
            <abp-column size-md="_12" size-lg="_6">
                @await Component.InvokeAsync(typeof(AuditLoggingAverageExecutionDurationPerDayWidgetViewComponent))
            </abp-column>
        }
        @if (await WidgetManager.IsGrantedAsync(typeof(SaasEditionPercentageWidgetViewComponent)))
        {
            <abp-column size-md="_12" size-lg="_6">
                @await Component.InvokeAsync(typeof(SaasEditionPercentageWidgetViewComponent))
            </abp-column>
        }
        @if (await WidgetManager.IsGrantedAsync(typeof(SaasLatestTenantsWidgetViewComponent)))
        {
            <abp-column size-md="_12" size-lg="_6">
                @await Component.InvokeAsync(typeof(SaasLatestTenantsWidgetViewComponent))
            </abp-column>
        }
    </abp-row>
</div>
```

Instead of having specific widgets let's build a widgets contributor for that Page, after we complete the implementatoin we should be able to load the widgets on the dashboard from contributrs:

```cs
<div id="HostDashboardWidgetsArea" data-widget-filter="#DashboardFilterForm">
    <abp-row>
        @foreach (var widget in (await hostDashboardManager.GetWidgetsAsync()))
        {
            if (await WidgetManager.IsGrantedAsync(widget.WidgetType))
            {
                <abp-column size-md="_12" size-lg="_6">
                    @await Component.InvokeAsync(widget.WidgetType)
                </abp-column>
            }
        }
    </abp-row>
</div>
```

we will start with the *ComponentContext* and *Component Model*:

```cs
// DashboardWidget.cs

using System;

namespace Abp.ContributorPatternExample.Web.HostDashboardManagement
{
    public class DashboardWidget
    {
        public Type WidgetType { get; set; }
    }
}
```

```cs
// HostDashboardPageContext.cs

using System;
using System.Collections.Generic;
using Volo.Abp.DependencyInjection;

namespace Abp.ContributorPatternExample.Web.HostDashboardManagement
{
    public class HostDashboardPageContext : IServiceProviderAccessor
    {
        public List<DashboardWidget> Widgets { get; }

        public IServiceProvider ServiceProvider { get; private set; }

        public HostDashboardPageContext(IServiceProvider serviceProvider)
        {
            Widgets = new List<DashboardWidget>();
            ServiceProvider = serviceProvider;

        }

        public void AddWidget(Type dashboardWidget)
        {
            Widgets.Add(new DashboardWidget
            {
                WidgetType = dashboardWidget,
            });
        }
    }
}
```

Now let's defin the interface of the contributor:

```cs
// IHostDashboardPageContributor.cs

using System.Threading.Tasks;

namespace Abp.ContributorPatternExample.Web.HostDashboardManagement
{
    public interface IHostDashboardPageContributor
    {
        Task ConfigureAsync(HostDashboardPageContext context);
    }
}
```

We will create the *Contributor Manager* and *Options* to load and configure the contributors:

```cs
//HostDashboardPageOptions.cs

using System.Collections.Generic;

namespace Abp.ContributorPatternExample.Web.HostDashboardManagement
{
    public class HostDashboardPageOptions
    {
        public List<IHostDashboardPageContributor> Contributors { get; }

        public HostDashboardPageOptions() 
        {
            Contributors = new List<IHostDashboardPageContributor>();
        }
    }
}
```


```cs
// HostDashboardPageManager.cs

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Abp.ContributorPatternExample.Web.HostDashboardManagement
{
    public class HostDashboardPageManager : IScopedDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public HostDashboardPageManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected virtual async Task<HostDashboardPageContext> ConfigureAsync() 
        {
            var context = new HostDashboardPageContext(_serviceProvider);
            var contributors = _serviceProvider.GetRequiredService<IOptions<HostDashboardPageOptions>>().Value.Contributors;

            foreach (var contributor in contributors)
            {
                await contributor.ConfigureAsync(context);
            }

            return context;
        }

        public virtual async Task<IReadOnlyList<DashboardWidget>> GetWidgetsAsync()
        {
            var context = await ConfigureAsync();

            return context.Widgets.AsReadOnly();
        }
    }
}
```

By this steps we created the structor of Contributor Pattern, now let' use this pattern, we will create a default contributor and configure `HostDashboardPageOptions` in the module:

```cs
// DefaultHostDashboardPageContributor.cs

using System.Threading.Tasks;
using Volo.Abp.AuditLogging.Web.Pages.Shared.Components.AverageExecutionDurationPerDayWidget;
using Volo.Abp.AuditLogging.Web.Pages.Shared.Components.ErrorRateWidget;
using Volo.Saas.Host.Pages.Shared.Components.SaasEditionPercentageWidget;
using Volo.Saas.Host.Pages.Shared.Components.SaasLatestTenantsWidget;

namespace Abp.ContributorPatternExample.Web.HostDashboardManagement
{
    public class DefaultHostDashboardPageContributor : IHostDashboardPageContributor
    {
        public async Task ConfigureAsync(HostDashboardPageContext context)
        {
            context.AddWidget(typeof(AuditLoggingErrorRateWidgetViewComponent));
            context.AddWidget(typeof(AuditLoggingAverageExecutionDurationPerDayWidgetViewComponent));
            context.AddWidget(typeof(SaasEditionPercentageWidgetViewComponent));
            context.AddWidget(typeof(SaasLatestTenantsWidgetViewComponent));
        }
    }
}
```

```cs
// ContributorPatternExampleWebModule.cs

Configure<HostDashboardPageOptions>(options =>
{
    options.Contributors.Add(new DefaultHostDashboardPageContributor());
});
```


Now we ready to use this on the `HostDashboard` page:

```html

<!-- Inject Contributor Manager -->

@inject HostDashboardPageManager hostDashboardManager 

<div id="HostDashboardWidgetsArea" data-widget-filter="#DashboardFilterForm">
    <abp-row>
        <!-- render contributors widgets -->
        @foreach (var widget in (await hostDashboardManager.GetWidgetsAsync()))
        {
            if (await WidgetManager.IsGrantedAsync(widget.WidgetType))
            {
                <abp-column size-md="_12" size-lg="_6">
                    @await Component.InvokeAsync(widget.WidgetType)
                </abp-column>
            }
        }
    </abp-row>
</div>
```


# last notes (Personal preference)

Instead of consuming the *Component Context* directly like in the `SettingPageCreationContext` or consume the *Component Model* by injecting the *Contributor Manager* like in `MenuManager` or our last example `HostDashboardPageManager`, I prefer to create a consumer intefrace. By this hide the creation context details required for contributor from the consumers. and make this model immutable from the consumer side.

```cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Abp.ContributorPatternExample.Web.HostDashboardManagement
{
    public class HostDashboardPageContributorClient : IScopedDependency
    {
        private readonly HostDashboardPageManager _manager;

        public HostDashboardPageContributorClient(HostDashboardPageManager hostDashboardPageManager)
        {
            _manager = hostDashboardPageManager;
        }

        public virtual async Task<IReadOnlyList<DashboardWidget>> GetWidgetsAsync()
        {
            var context = await _manager.ConfigureAsync();

            return context.Widgets.AsReadOnly();
        }
    }
}
```

# Summerize

Contributor pattern can be a greate tool to implement extensibility in your modules, by applying Contributor Pattern you can offer a easy way to build rich components getting the benfites of other modules contributions. 

The link of the example source code [Source Code](https://github.com/QaisAlkhateeb/QKhateeb.AbpIO.Posts/tree/master/Example)