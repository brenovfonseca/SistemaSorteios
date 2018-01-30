using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoimainConcurso.Entidades
{
    public class Sorteio
    {
        public Sorteio(List<int> numerosSorteados)
        {
            NumerosSorteados = numerosSorteados;
        }
        public int IDSorteio { get; set; }

        public List<int> NumerosSorteados { get; set; }
    }
}