using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoimainConcurso
{
    using DoimainConcurso;
    using Contratos;
    using DoimainConcurso.Entidades;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Repositorios;

    namespace ConsoleApplication1
    {
        class Program
        {
            static void Main(string[] args)
            {
                const string nomeConcurso = "Mega Sena Janeiro";

                Concurso concurso = new Concurso(new MegaSena(6, 1, 60));
                concurso.NomeConcurso = nomeConcurso;

                IConcursoRepository _concursoRepository = new ConcursoRepository();

                IConcursoDomain _concursoDomain = new ConcursoDomain(_concursoRepository);

                string mensagem = string.Empty;

                _concursoDomain.CriarConcurso(concurso, out mensagem);

                if (!ValidarMensagem(mensagem))
                {
                    Console.WriteLine(mensagem);
                    Console.ReadKey();
                    return;
                }

                _concursoDomain.GerarJogosAleatorios(nomeConcurso, 1000, out mensagem);

                if (!ValidarMensagem(mensagem))
                {
                    Console.WriteLine(mensagem);
                    Console.ReadKey();
                    return;
                }

                Jogo jogo = new Jogo();
                jogo.NumeroCartao = Guid.NewGuid();
                jogo.NumerosJogo = new List<int>(6);
                jogo.NumerosJogo.Add(1);
                jogo.NumerosJogo.Add(30);
                jogo.NumerosJogo.Add(39);
                jogo.NumerosJogo.Add(40);
                jogo.NumerosJogo.Add(42);
                jogo.NumerosJogo.Add(44);

                _concursoDomain.CadastrarNovoJogo(jogo, nomeConcurso, out mensagem);

                if (!ValidarMensagem(mensagem))
                {
                    Console.WriteLine(mensagem);
                    Console.ReadKey();
                    return;
                }


                _concursoDomain.ExecutarSorteio(nomeConcurso, out mensagem);

                if (!ValidarMensagem(mensagem))
                {
                    Console.WriteLine(mensagem);
                    Console.ReadKey();
                    return;
                }

                _concursoDomain.ProcessarCartoes(nomeConcurso, out mensagem);

                if (!ValidarMensagem(mensagem))
                {
                    Console.WriteLine(mensagem);
                    Console.ReadKey();
                    return;
                }


                Concurso concursoContext = _concursoDomain.BuscarConcursoPorNome(nomeConcurso);

                MegaSena resultado = (MegaSena)_concursoDomain.VerificarAcertadores(nomeConcurso, out mensagem);

                Console.WriteLine(string.Format("Concurso - {0}", concursoContext.NomeConcurso));


                Console.WriteLine("");
                Console.WriteLine("|||||||");
                Console.WriteLine("");


                Console.WriteLine("Numeros Sorteados");

                concurso.Sorteio.NumerosSorteados.ForEach(delegate (int numero)
                {
                    Console.WriteLine(numero);
                });

                Console.WriteLine("");
                Console.WriteLine("|||||||");
                Console.WriteLine("");



                Console.WriteLine("GANHADORES");
                Console.WriteLine("");
                Console.WriteLine("|||||||");
                Console.WriteLine("");

                Console.WriteLine("Sena");

                if (resultado.AcertadoresSena == null || resultado.AcertadoresSena.Count == 0)
                    Console.WriteLine("Não houve acertadores.");
                else Console.WriteLine(string.Format("Quantidade Acertadores: {0}", resultado.AcertadoresSena.Count));


                foreach (var item in resultado.AcertadoresSena)
                {
                    Console.WriteLine(string.Format("Cartão número - {0}", item.NumeroCartao));
                }

                Console.WriteLine("");
                Console.WriteLine("|||||||");
                Console.WriteLine("");

                Console.WriteLine("Quina");

                if (resultado.AcertadoresQuina == null || resultado.AcertadoresQuina.Count == 0)
                    Console.WriteLine("Não houve acertadores.");
                else Console.WriteLine(string.Format("Quantidade Acertadores: {0}", resultado.AcertadoresQuina.Count));



                foreach (var item in resultado.AcertadoresQuina)
                {
                    Console.WriteLine(string.Format("Cartão número - {0}", item.NumeroCartao));
                }

                Console.WriteLine("");
                Console.WriteLine("|||||||");
                Console.WriteLine("");
                Console.WriteLine("Quadra");

                if (resultado.AcertadoresQuadra == null || resultado.AcertadoresQuadra.Count == 0)               
                    Console.WriteLine("Não houve acertadores.");
                else Console.WriteLine(string.Format("Quantidade Acertadores: {0}", resultado.AcertadoresQuadra.Count));


                foreach (var item in resultado.AcertadoresQuadra)
                {
                    Console.WriteLine(string.Format("Cartão número - {0}", item.NumeroCartao));
                }
                Console.WriteLine("");
                Console.WriteLine("|||||||");
                Console.WriteLine("");

                Console.ReadKey();

            }

            private static bool ValidarMensagem(string mensagem)
            {
                if (!string.IsNullOrWhiteSpace(mensagem))
                {
                    Console.WriteLine(mensagem);
                    return false;
                }

                return true;
            }
        }
    }
}
