using curso_api.Businnes.Entities;
using System.Collections.Generic;

namespace curso_api.Businnes.Repositories
{
    public interface ICourseRepository
    {
        void Add(Course course);

        void Commit();

        IList<Course> GetCoursesPerUser(int codeUser);

    }
}
