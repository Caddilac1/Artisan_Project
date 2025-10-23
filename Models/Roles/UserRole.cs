namespace ArtisanMarketplace.Models.Roles
{
    public class UserRole : BaseRole
    {
        public UserRole()
        {
            RoleType = RoleTypes.User;
        }

        public override string GetRoleDisplayName() => "User";

        public override List<string> GetPermissions() =>
            new List<string>(Permissions.BasicUserPermissions);

        public override int GetPriorityLevel() => 10;
    }
}
