namespace ArtisanMarketplace.Models.Roles
{
    public class ModeratorRole : BaseRole
    {
        public ModeratorRole()
        {
            RoleType = RoleTypes.Moderator;
        }

        public override string GetRoleDisplayName() => "Moderator";

        public override List<string> GetPermissions() =>
            Permissions.BasicUserPermissions
                .Concat(Permissions.ModeratorPermissions)
                .Distinct()
                .ToList();

        public override int GetPriorityLevel() => 50;
    }
}
