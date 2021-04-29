using curso_api.Businnes.Entities;

namespace curso_api.Businnes.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        void Commit();
        User GetUser(string username);
    }
}
