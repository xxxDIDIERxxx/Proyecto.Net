using ASP.NETCoreIdentityCustom.Areas.Identity.Data;
using ASP.NETCoreIdentityCustom.Core.Repositories;

namespace ASP.NETCoreIdentityCustom.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<ApplicationUser> GetUsers()
        {
            return _context.Users.ToList();
        }

        public ApplicationUser GetUser(string id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public ApplicationUser UpdateUser(ApplicationUser user)
        {
             _context.Update(user);
             _context.SaveChanges();

             return user;
        }

        public void DeleteUser(ApplicationUser user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public int GetRegisteredUsersCount()
        {
            return _context.Users.Count();
        }

    }
}
