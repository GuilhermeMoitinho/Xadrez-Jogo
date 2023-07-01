using System;
using Xadrez_Console.Tabuleiro;
using Xadrez_Console.Xadrez;

namespace Xadrez_Console {
    class Program {
        static void Main(string[] args)
        {

            try
            { 
                PartidaDeXadrez partida = new PartidaDeXadrez();

                while (!partida.Terminada)
                {
                    try
                    {
                        Console.Clear();
                        Tela.ImprimirTabuleiro(partida.tab);
                        Console.WriteLine("Turno: " + partida.Turno);
                        Console.WriteLine("Aguardando a jogada: " + partida.JogadorAtual);

                        Console.WriteLine();

                        Console.Write("Origem: ");
                        Posicao origem = Tela.LerPosicaoXadrez().toPosicao();
                        partida.ValidarPosicaoDeOrigem(origem);

                        bool[,] PosicoesPossiveis = partida.tab.peca(origem).movimentoPossiveis();

                        Console.Clear();
                        Tela.ImprimirTabuleiro(partida.tab, PosicoesPossiveis);

                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Posicao destino = Tela.LerPosicaoXadrez().toPosicao();
                        partida.ValidarPosicaoDeDestino(origem, destino);

                        partida.RealizaJogada(origem, destino);
                    }
                    catch (ExcessaoTabuleiro e)
                    {
                        Console.WriteLine(e.Message );
                        Console.ReadLine();
                    }
                }

               
            }
            catch (ExcessaoTabuleiro e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();

        }

     }
}


