using Tabuleiro;
using Xadrez_Console.Tabuleiro;
using Xadrez_Console.Xadrez;

namespace Xadrez_Console {
    class Program {
        static void Main(string[] args)
        {

           
            Tab tabuleiro = new Tab(8, 8);

            tabuleiro.ColocarPeca(new Torre(Cor.Preta, tabuleiro), new Posicao(0,0));
            tabuleiro.ColocarPeca(new Torre(Cor.Preta, tabuleiro),new Posicao(1, 3));
            tabuleiro.ColocarPeca(new Rei(Cor.Preta, tabuleiro),new Posicao(2, 4));

            Tela.ImprimirTabuleiro( tabuleiro );

            Console.ReadLine();

        }

     }
}


