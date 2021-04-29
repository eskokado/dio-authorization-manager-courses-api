using curso_api.Businnes.Entities;
using curso_api.Businnes.Repositories;
using curso_api.Configurations;
using curso_api.Filters;
using curso_api.Models;
using curso_api.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace curso_api.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;

        public UserController(
            IUserRepository userRepository, 
            IAuthenticationService authenticationService)
        {

            _userRepository = userRepository;
            _authenticationService = authenticationService;

        }


        /// <summary>
        /// Este serviço permite autenticar um usuário cadastrado e ativo.
        /// </summary>
        /// <param name="loginViewModelInput">View model do login</param>
        /// <returns>Retorna ok, dados do usuario e o token em caso</returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(FieldValidatesViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErrorGenericViewModel))]
        [HttpPost]
        [Route("login")]
        [ValidationModelStateCustom]
        public IActionResult Login(LoginViewModelInput loginViewModelInput)
        {
            User user = _userRepository.GetUser(loginViewModelInput.Username);

            if (user == null)
            {
                return BadRequest("Houve um erro ao tentar acessar.");
            }

            var userViewModelOutput = new UserViewModelOutput()
            {
                Code = user.Code,
                Username = user.Username,
                Email = user.Email
            };
            
            var token = _authenticationService.GenerateToken(userViewModelOutput);

            return Ok(new
            {
                Token = token,
                User = userViewModelOutput
            });
        }

        /// <summary>
        /// Este serviço para registrar um novo usuário.
        /// </summary>
        /// <param name="registerViewModelInput">View model do login</param>
        /// <returns>Retorna ok, dados do usuario e o token em caso</returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao registrar", Type = typeof(RegisterViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(FieldValidatesViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErrorGenericViewModel))]
        [HttpPost]
        [Route("register")]
        [ValidationModelStateCustom]
        public IActionResult Register(RegisterViewModelInput registerViewModelInput)
        {

            //var migrationsPendents = context.Database.GetAppliedMigrations();
            //if (migrationsPendents.Count() > 0)
            //{
                //context.Database.Migrate();
            //}

            var user = new User();
            user.Username = registerViewModelInput.Username;
            user.Email = registerViewModelInput.Email;
            user.Password = registerViewModelInput.Password;

            _userRepository.Add(user);
            _userRepository.Commit();

            return Created("", registerViewModelInput);
        }
    }
}
