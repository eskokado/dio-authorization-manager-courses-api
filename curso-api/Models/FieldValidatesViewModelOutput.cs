using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace curso_api.Models
{
    public class FieldValidatesViewModelOutput
    {
        public IEnumerable<string> Erros { get; private set; }

        public FieldValidatesViewModelOutput(IEnumerable<string> erros)
        {
            Erros = erros;
        }
    }
}
