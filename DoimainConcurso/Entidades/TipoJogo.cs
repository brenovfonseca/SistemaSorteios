using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoimainConcurso
{
    public class TipoJogo
    {
        private TipoJogo()
        {

        }
        public int QuantidadeNumeros { get; set; }
        public int IntervaloInicial { get; set; }
        public int IntervaloFinal { get; set; }

        public TipoJogo(int quantidadeNumeros, int intervaloInicial, int intervaloFinal)
        {
            QuantidadeNumeros = quantidadeNumeros;
            IntervaloInicial = intervaloInicial;
            IntervaloFinal = intervaloFinal;
        }
    }
}
