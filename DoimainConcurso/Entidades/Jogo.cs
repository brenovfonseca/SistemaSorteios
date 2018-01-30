using DoimainConcurso.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoimainConcurso.Entidades
{
    public class Jogo
    {
        public Jogo()
        {

        }

        public int IDJogo { get; set; }
        public List<int> NumerosJogo { get; set; }
        public Guid NumeroCartao { get; set; }
        public int  QuantidadeNumerosAcertados { get; set; }
    }
}
