using ASP.NETCoreIdentityCustom.Areas.Identity.Data;

namespace ASP.NETCoreIdentityCustom.Core.Repositories
{
    public interface IUserRepository
    {
        ICollection<ApplicationUser> GetUsers();

        ApplicationUser GetUser(string id);

        ApplicationUser UpdateUser(ApplicationUser user);

        void DeleteUser(ApplicationUser user);

        int GetRegisteredUsersCount();
    }
}
