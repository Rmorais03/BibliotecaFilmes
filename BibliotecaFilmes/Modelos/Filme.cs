using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaFilmes.Modelos
{
    public class Filme
    {
        public int Idfilme;
        public string Titulo;
        public int Ano;
        public int Duracao; // MINUTOS!!
        public List<string> Genero;
        public int Idrealizador;
    }
}   
