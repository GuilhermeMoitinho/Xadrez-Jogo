using Tabuleiro;
using Xadrez_Console.Tabuleiro;

namespace Xadrez_Console {
    class Program {
        static void Main(string[] args)
        {

           
            Tab tabuleiro = new Tab(8, 8);

            Tela.ImprimirTabuleiro( tabuleiro );

            Console.ReadLine();

        }

     }
}


