using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaFilmes.Modelos
{

    public class Festival
    {
        public int Idfestival;
        public string Nome;
        public string Local;
        public DateTime Datainicio; // "dia-mes-ano" //https://learn.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings
        public DateTime Datafim;    // "dia-mes-ano" // https://blog.ndepend.com/csharp-datetime-format/
        public List<string> Filmesinscritos;
    }
}
