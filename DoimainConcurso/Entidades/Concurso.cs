using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoimainConcurso.Entidades
{
    public class Concurso
    {
        private Concurso()
        {

        }
        public Concurso(TipoJogo tipoJogo)
        {
            TipoJogo = tipoJogo;
        }

        public int IDConcurso { get; set; }

        public string NomeConcurso { get; set; }

        public TipoJogo TipoJogo { get; set; }

        public IEnumerable<Jogo> Jogos { get; set; }

        public Sorteio Sorteio { get; set; }

    }
}
