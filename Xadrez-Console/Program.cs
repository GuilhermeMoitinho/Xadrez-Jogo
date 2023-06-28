using Tabuleiro;
using Xadrez_Console.Tabuleiro;
using Xadrez_Console.Xadrez;

namespace Xadrez_Console {
    class Program {
        static void Main(string[] args)
        {

           PosicaoXadrez pos = new PosicaoXadrez('a', 1);

            Console.WriteLine(pos.toPosicao());
            Console.ReadLine();

        }

     }
}


