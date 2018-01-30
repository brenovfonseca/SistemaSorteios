using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoimainConcurso.Entidades
{
    public class MegaSena : TipoJogo
    {
        public MegaSena(int quantidadeNumeros, int intervaloInicial, int intervaloFinal)
                : base(quantidadeNumeros, intervaloInicial, intervaloFinal) { }
        public List<Jogo> AcertadoresQuadra { get; set; }

        public List<Jogo> AcertadoresQuina { get; set; }

        public List<Jogo> AcertadoresSena { get; set; }
    }
}
