using curso_api.Filters;
using curso_api.Models;
using curso_api.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace curso_api.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
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
            return Ok(loginViewModelInput);
        }

        /// <summary>
        /// Este serviço para registrar um novo usuário.
        /// </summary>
        /// <param name="registerViewModelInput">View model do login</param>
        /// <returns>Retorna ok, dados do usuario e o token em caso</returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao cadastrar", Type = typeof(RegisterViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(FieldValidatesViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErrorGenericViewModel))]
        [HttpPost]
        [Route("register")]
        [ValidationModelStateCustom]
        public IActionResult Register(RegisterViewModelInput registerViewModelInput)
        {
            return Created("", registerViewModelInput);
        }
    }
}
