using WebMatrix.WebData;

namespace Accounting
{
    public static class DefaultUsers
    {
        private const string AdminUsername = "Admin";
        private const string AdminPassword = "probAb1942glh";
        private const string AdministratorsRoleName = "Administrators";

        public static void Register()
        {
            if (WebSecurity.UserExists(AdminUsername))
                return;

            var roles = (SimpleRoleProvider) System.Web.Security.Roles.Provider;

            WebSecurity.CreateUserAndAccount(AdminUsername, AdminPassword);

            roles.CreateRole(AdministratorsRoleName);
            roles.AddUsersToRoles(new[] {AdminUsername}, new[] {AdministratorsRoleName});
        }
    }
}