using DoimainConcurso.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoimainConcurso.Contratos
{

    public interface IConcursoDomain
    {
        void CriarConcurso(Concurso concurso, out string mensagem);
        bool GerarJogosAleatorios(string nomeConcurso, int quantidadeJogos, out string mensagem);
        bool CadastrarNovoJogo(Jogo jogo, string nomeConcurso, out string mensagem);
        void ExecutarSorteio(string nomeConcurso, out string mensagem);
        TipoJogo VerificarAcertadores(string nomeConcurso, out string mensagem);
        void ProcessarCartoes(string nomeConcurso, out string mensagem);
        Concurso BuscarConcursoPorNome(string nomeConcurso);
    }
}
