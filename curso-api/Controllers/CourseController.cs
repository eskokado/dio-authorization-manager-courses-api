using curso_api.Models.Courses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace curso_api.Controllers
{
    [Route("api/v1/courses")]
    [ApiController]
    [Authorize]
    public class CourseController : ControllerBase
    {
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao cadastrar um curso", Type = typeof(CourseViewModelInput))]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(CourseViewModelInput courseViewModelInput)
        {
            var codeUser = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            return Created("", courseViewModelInput);
        }

        [SwaggerResponse(statusCode: 200, description: "Sucesso ao obter os cursos")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var cursos = new List<CourseViewModelOutput>();

            // var codeUser = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value).ToString(),
            cursos.Add(new CourseViewModelOutput()
            {
                Login = "",
                Name = "Java",
                Description = "Descrição do Curso Java"
            });
            cursos.Add(new CourseViewModelOutput()
            {
                Login = "",
                Name = "CSharp",
                Description = "Descrição do Curso CSharp"
            });
            cursos.Add(new CourseViewModelOutput()
            {
                Login = "",
                Name = "Javascript",
                Description = "Descrição do Curso Javascript"
            });
            return Ok(cursos);
        }
    }


}
