using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace curso_api.Businnes.Entities
{
    public class Course
    {
        public int Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CodeUser { get; set; }

        public virtual User User { get; set; }
    }
}
