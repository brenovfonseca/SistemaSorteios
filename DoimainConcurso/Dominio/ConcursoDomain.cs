
using DoimainConcurso.Contratos;
using DoimainConcurso.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoimainConcurso
{
    public class ConcursoDomain : IConcursoDomain
    {
        private readonly IConcursoRepository _concursoRepository;

        public ConcursoDomain(IConcursoRepository concursoRepository)
        {
            _concursoRepository = concursoRepository;
        }

        public bool CadastrarNovoJogo(Jogo jogo, string nomeConcurso, out string mensagem)
        {
            Concurso concurso = BuscarConcursoPorNome(nomeConcurso);

            if (concurso == null)
            {
                mensagem = "Concurso não cadastrado";
                return false;
            }

            _concursoRepository.CadastrarNovoJogo(jogo, nomeConcurso);

            mensagem = string.Empty;
            return true;

        }

        public void CriarConcurso(Concurso concurso, out string mensagem)
        {
            Concurso concursoAtual = BuscarConcursoPorNome(concurso.NomeConcurso);

            if (concursoAtual == null)
            {
                _concursoRepository.CadastrarNovoConcurso(concurso);
                mensagem = string.Empty;
                return;
            }

            mensagem = "Concurso já cadastrado";
        }

        public void ExecutarSorteio(string nomeConcurso, out string mensagem)
        {
            Concurso concurso = BuscarConcursoPorNome(nomeConcurso);

            if (concurso == null)
            {
                mensagem = "Concurso não cadastrado";
                return;
            }


            if (_concursoRepository.ConcursoPossuiSorteio(nomeConcurso))
            {
                mensagem = "Concurso já foi sorteado";
                return;
            }


            List<int> numerosSorteados = new List<int>();

            Sorteio sorteio = new Sorteio(numerosSorteados);


            _concursoRepository.CadastrarSorteio(sorteio, nomeConcurso);


            for (int i = 0; i < concurso.TipoJogo.QuantidadeNumeros; i++)
            {
                RealizarSorteioNumero(concurso, sorteio);

            }

            _concursoRepository.CadastrarSorteio(sorteio, concurso.NomeConcurso);

            mensagem = string.Empty;

        }

        private void RealizarSorteioNumero(Concurso concurso, Sorteio sorteio)
        {
            int numeroSorteado = 0;

            while (numeroSorteado == 0)
            {
                Random numero = new Random();

                numeroSorteado = numero.Next(concurso.TipoJogo.IntervaloInicial, concurso.TipoJogo.IntervaloFinal);

                if (sorteio.NumerosSorteados.Contains(numeroSorteado))
                {
                    numeroSorteado = 0;
                    continue;
                }

                sorteio.NumerosSorteados.Add(numeroSorteado);
            }
        }

        private void GerarNovoNumeroJogo(Concurso concurso, Jogo jogo)
        {
            int numeroSorteado = 0;

            while (numeroSorteado == 0)
            {
                Random numero = new Random();

                numeroSorteado = numero.Next(concurso.TipoJogo.IntervaloInicial, concurso.TipoJogo.IntervaloFinal);

                if (jogo.NumerosJogo.Contains(numeroSorteado))
                {
                    numeroSorteado = 0;
                    continue;
                }

                jogo.NumerosJogo.Add(numeroSorteado);
            }
        }

        private Jogo GerarJogoAleatorio(Concurso concurso)
        {
            Jogo jogo = new Jogo();

            jogo.IDJogo = _concursoRepository.RetornarSequencialJogo(concurso.NomeConcurso);
            jogo.NumeroCartao = Guid.NewGuid();
            jogo.NumerosJogo = new List<int>();

            for (int i = 0; i < concurso.TipoJogo.QuantidadeNumeros; i++)
            {
                GerarNovoNumeroJogo(concurso, jogo);
            }

            return jogo;
        }

        public bool GerarJogosAleatorios(string nomeConcurso, int quantidadeJogos, out string mensagem)
        {
            Concurso concurso = BuscarConcursoPorNome(nomeConcurso);

            if (concurso == null)
            {
                mensagem = "Concurso não cadastrado";
                return false;
            }

            for (int i = 0; i < quantidadeJogos; i++)
            {
                CadastrarNovoJogo(GerarJogoAleatorio(concurso), concurso.NomeConcurso, out mensagem);
            }
            mensagem = string.Empty;
            return true;
        }

        public void ProcessarCartoes(string nomeConcurso, out string mensagem)
        {
            Concurso concurso = BuscarConcursoPorNome(nomeConcurso);

            if (concurso == null)
            {
                mensagem = "Concurso não cadastrado";
                return;
            }

            if (concurso.Sorteio == null)
            {
                mensagem = "Concurso não possui sorteio";
                return;
            }

            concurso.Jogos.ToList().ForEach(delegate (Jogo jogo)
            {
                for (int i = 0; i < concurso.TipoJogo.QuantidadeNumeros; i++)
                {
                    if (concurso.Sorteio.NumerosSorteados.Contains(jogo.NumerosJogo[i]))
                    {
                        jogo.QuantidadeNumerosAcertados++;
                    }
                }
            });
            mensagem = string.Empty;
            return;
        }

        public TipoJogo VerificarAcertadores(string nomeConcurso, out string mensagem)
        {
            Concurso concurso = BuscarConcursoPorNome(nomeConcurso);

            if (concurso == null)
            {
                mensagem = "Concurso não cadastrado";
                return null;
            }

            ((MegaSena)concurso.TipoJogo).AcertadoresQuadra = concurso.Jogos.ToList().Where(j => j.QuantidadeNumerosAcertados == 4).ToList();
            ((MegaSena)concurso.TipoJogo).AcertadoresQuina = concurso.Jogos.ToList().Where(j => j.QuantidadeNumerosAcertados == 5).ToList();
            ((MegaSena)concurso.TipoJogo).AcertadoresSena = concurso.Jogos.ToList().Where(j => j.QuantidadeNumerosAcertados == 6).ToList();


            mensagem = string.Empty;
            return concurso.TipoJogo;
        }

        public Concurso BuscarConcursoPorNome(string nomeConcurso)
        {
            return _concursoRepository.ObterConcursoPorNome(nomeConcurso); ;
        }
    }
}
