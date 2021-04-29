using curso_api.Businnes.Entities;
using curso_api.Businnes.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace curso_api.InfraStructure.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CourseDbContext _context;

        public CourseRepository(CourseDbContext context)
        {
            _context = context;
        }

        public void Add(Course course)
        {
            _context.Courses.Add(course);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public IList<Course> GetCoursesPerUser(int codeUser)
        {
            return _context.Courses.Include(i => i.User).Where(w => w.CodeUser == codeUser).ToList();
        }
    }
}
