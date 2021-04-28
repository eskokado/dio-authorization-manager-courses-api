using curso_api.Businnes.Entities;
using curso_api.Filters;
using curso_api.InfraStructure.Data;
using curso_api.Models;
using curso_api.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace curso_api.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : ControllerBase
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

            var userViewModelOutput = new UserViewModelOutput()
            {
                Code = 1,
                Username = "eskokodo",
                Email = "eskokado@email.com"
            };


            var secret = Encoding.ASCII.GetBytes("DigitalInnovationOneTakeBlipFullstack");
            var symmetricSecurityKey = new SymmetricSecurityKey(secret);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userViewModelOutput.Code.ToString()),
                    new Claim(ClaimTypes.Name, userViewModelOutput.Username.ToString()),
                    new Claim(ClaimTypes.Email, userViewModelOutput.Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenGenerated = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(tokenGenerated);

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
            var optionsBuilder = new DbContextOptionsBuilder<CourseDbContext>();
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=DB_COURSES;Persist Security Info=True;User ID=sa;Password=sa@123456");
            CourseDbContext context = new CourseDbContext(optionsBuilder.Options);

            var migrationsPendents = context.Database.GetAppliedMigrations();
            if (migrationsPendents.Count() > 0)
            {
                context.Database.Migrate();
            }

            var user = new User();
            user.Username = registerViewModelInput.Username;
            user.Email = registerViewModelInput.Email;
            user.Password = registerViewModelInput.Password;

            context.Users.Add(user);
            context.SaveChanges();

            return Created("", registerViewModelInput);
        }
    }
}
