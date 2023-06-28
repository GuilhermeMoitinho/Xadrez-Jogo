using Tabuleiro;
using Xadrez_Console.Tabuleiro;
using Xadrez_Console.Xadrez;

namespace Xadrez_Console {
    class Program {
        static void Main(string[] args)
        {

            try
            {
                Tab tabuleiro = new Tab(8, 8);

                tabuleiro.ColocarPeca(new Torre(Cor.Preta, tabuleiro), new Posicao(0, 0));
                tabuleiro.ColocarPeca(new Torre(Cor.Preta, tabuleiro), new Posicao(1, 3));
                tabuleiro.ColocarPeca(new Rei(Cor.Preta, tabuleiro), new Posicao(1, -9));

                Tela.ImprimirTabuleiro(tabuleiro);
            }
            catch (ExcessaoTabuleiro e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();

        }

     }
}


