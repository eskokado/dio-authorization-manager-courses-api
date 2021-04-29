using curso_api.Businnes.Entities;
using curso_api.Businnes.Repositories;
using System.Linq;

namespace curso_api.InfraStructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CourseDbContext _context;

        public UserRepository(CourseDbContext context)
        {
            _context = context;
        }
        
        public void Add(User user)
        {
            _context.Users.Add(user);            
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public User GetUser(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }
    }
}
