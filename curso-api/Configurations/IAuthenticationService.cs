using curso_api.Models;

namespace curso_api.Configurations
{
    public interface IAuthenticationService
    {
        string GenerateToken(UserViewModelOutput userViewModelOutput);
    }
}
