namespace Abp.ContributorPatternExample.Permissions;

public static class ContributorPatternExamplePermissions
{
    public const string GroupName = "ContributorPatternExample";

    public static class Dashboard
    {
        public const string DashboardGroup = GroupName + ".Dashboard";
        public const string Host = DashboardGroup + ".Host";
        public const string Tenant = DashboardGroup + ".Tenant";
    }

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
}
