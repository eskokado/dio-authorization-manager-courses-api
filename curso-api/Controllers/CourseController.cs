using curso_api.Businnes.Entities;
using curso_api.Businnes.Repositories;
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
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [SwaggerResponse(statusCode: 201, description: "Sucesso ao cadastrar um curso", Type = typeof(CourseViewModelInput))]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(CourseViewModelInput courseViewModelInput)
        {
            Course course = new Course();
            course.Name = courseViewModelInput.Name;
            course.Description = courseViewModelInput.Description;
            var codeUser = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            course.CodeUser = codeUser;
            _courseRepository.Add(course);
            _courseRepository.Commit();
            return Created("", courseViewModelInput);
        }

        [SwaggerResponse(statusCode: 200, description: "Sucesso ao obter os cursos")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var codeUser = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            var courses = _courseRepository.GetCoursesPerUser(codeUser)
                .Select(s => new CourseViewModelOutput()
                {
                    Login = s.User.Username,
                    Name = s.Name,
                    Description = s.Description
                });
            return Ok(courses);
        }
    }


}
