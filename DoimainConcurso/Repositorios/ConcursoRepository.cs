using System;
using System.Collections.Generic;
using System.Linq;
using DoimainConcurso.Entidades;
using DoimainConcurso.Contratos;

namespace DoimainConcurso.Repositorios
{
    public class ConcursoRepository : IConcursoRepository
    {
        public ConcursoRepository()
        {
            Database.Concursos = new List<Concurso>();
        }

        public void AtualizarConcurso(Concurso concurso)
        {
            throw new NotImplementedException();
        }

        public void CadastrarNovoConcurso(Concurso concurso)
        {
            Database.Concursos.Add(concurso);
            Database.Concursos.Where(c => c.NomeConcurso == concurso.NomeConcurso).FirstOrDefault().Jogos = new List<Jogo>();
            Database.Concursos.Where(c => c.NomeConcurso == concurso.NomeConcurso).FirstOrDefault().IDConcurso = RetornarSequencialConcurso();
        }

        public void CadastrarNovoJogo(Jogo jogo, string nomeConcurso)
        {
            List<Jogo> jogos = Database.Concursos.Where(c => c.NomeConcurso == nomeConcurso).FirstOrDefault().Jogos.ToList();



            jogos.Add(jogo);

            Database.Concursos.Where(c => c.NomeConcurso == nomeConcurso).FirstOrDefault().Jogos = jogos;
        }

        public void AtualizarJogo(Jogo jogo, string nomeConcurso)
        {
            Database.Concursos.ToList().Where(c => c.NomeConcurso == nomeConcurso).FirstOrDefault().Jogos.ToList().Where(J => J.NumeroCartao == jogo.NumeroCartao).FirstOrDefault().QuantidadeNumerosAcertados = jogo.QuantidadeNumerosAcertados;
        }

        public void CadastrarSorteio(Sorteio sorteio, string nomeConcurso)
        {
            Database.Concursos.Where(c => c.NomeConcurso == nomeConcurso).FirstOrDefault().Sorteio = sorteio;
        }

        public bool ConcursoPossuiSorteio(string nomeConcurso)
        {
            return Database.Concursos.Where(c => c.NomeConcurso == nomeConcurso).FirstOrDefault().Sorteio != null;
        }

        public Concurso ObterConcursoPorNome(string nomeConcurso)
        {
            return Database.Concursos.Where(c => c.NomeConcurso == nomeConcurso).FirstOrDefault();
        }

        public int RetornarSequencialJogo(string nomeConcurso)
        {
            return Database.Concursos.Where(c => c.NomeConcurso == nomeConcurso).FirstOrDefault().Jogos.Count() + 1;
        }

        public int RetornarSequencialConcurso()
        {
            return Database.Concursos.Count() + 1;
        }
    }
}
