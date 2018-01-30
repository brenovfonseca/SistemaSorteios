using DoimainConcurso;
using DoimainConcurso.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoimainConcurso.Contratos
{
    public interface IConcursoRepository
    {
        void CadastrarNovoConcurso(Concurso concurso);

        void AtualizarConcurso(Concurso concurso);

        void CadastrarNovoJogo(Jogo jogo, string nomeConcurso);

        void CadastrarSorteio(Sorteio sorteio, string nomeConcurso);

        Concurso ObterConcursoPorNome(string nomeConcurso);

        bool ConcursoPossuiSorteio(string nomeConcurso);

        int RetornarSequencialJogo(string nomeConcurso);

        int RetornarSequencialConcurso();
    }
}
