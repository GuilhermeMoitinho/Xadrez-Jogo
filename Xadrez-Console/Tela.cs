using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez_Console.Tabuleiro;

namespace Xadrez_Console
{
    internal class Tela
    {

        public static void ImprimirTabuleiro(Tab tabuleiro) {
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                for (int j = 0; j <  tabuleiro.Colunas; j++)
                {
                    if (tabuleiro.peca(i,j) == null)
                    {
                        Console.Write("- ");
                    }
                    else 
                    {
                        Console.Write(tabuleiro.peca(i, j) + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
