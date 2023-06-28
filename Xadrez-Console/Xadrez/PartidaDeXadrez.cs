using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabuleiro;
using Xadrez_Console.Tabuleiro;

namespace Xadrez_Console.Xadrez
{
    internal class PartidaDeXadrez
    {
        public Tab tab {get;private set;}
        private int Turno;
        private Cor JogadorAtual;
        public bool Terminada { get; private set; }



        public PartidaDeXadrez()
        {
            tab = new Tab(8,8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            ColocarPecas();
        }

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.RetirarPeca(origem);
            p.IncrementarQntMovimentos();
            Peca PecaCapturda = tab.RetirarPeca(destino);
            tab.ColocarPeca(p, destino);
        }

        private void ColocarPecas()
        {
            tab.ColocarPeca(new Torre(Cor.Branca, tab), new PosicaoXadrez('c', 1).toPosicao());
            tab.ColocarPeca(new Torre(Cor.Branca, tab), new PosicaoXadrez('c', 2).toPosicao());
            tab.ColocarPeca(new Torre(Cor.Branca, tab), new PosicaoXadrez('d', 2).toPosicao());
            tab.ColocarPeca(new Torre(Cor.Branca, tab), new PosicaoXadrez('e', 1).toPosicao());
            tab.ColocarPeca(new Torre(Cor.Branca, tab), new PosicaoXadrez('e', 2).toPosicao());
            tab.ColocarPeca(new Rei(Cor.Branca, tab), new PosicaoXadrez('d', 1).toPosicao());


            tab.ColocarPeca(new Torre(Cor.Preta, tab), new PosicaoXadrez('c', 7).toPosicao());
            tab.ColocarPeca(new Torre(Cor.Preta, tab), new PosicaoXadrez('c', 8).toPosicao());
            tab.ColocarPeca(new Torre(Cor.Preta, tab), new PosicaoXadrez('d', 7).toPosicao());
            tab.ColocarPeca(new Torre(Cor.Preta, tab), new PosicaoXadrez('e', 7).toPosicao());
            tab.ColocarPeca(new Torre(Cor.Preta, tab), new PosicaoXadrez('e', 8).toPosicao());
            tab.ColocarPeca(new Rei(Cor.Preta, tab), new PosicaoXadrez('d', 8).toPosicao());
        }
    }
}
