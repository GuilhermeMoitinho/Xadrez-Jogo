using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez_Console.Tabuleiro;

namespace Xadrez_Console.Xadrez
{
    internal class PartidaDeXadrez
    {
        public Tab tab {get;private set;}
        public int Turno { get;private set;}
        public Cor JogadorAtual { get;private set;}
        public bool Terminada { get; private set; }
        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturadas;
        public bool Xeque { get; private set; }



        public PartidaDeXadrez()
        {
            tab = new Tab(8,8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.RetirarPeca(origem);
            p.IncrementarQntMovimentos();
            Peca PecaCapturada = tab.RetirarPeca(destino);
            tab.ColocarPeca(p, destino);
            if (PecaCapturada != null)
            {
                Capturadas.Add(PecaCapturada);
            }
            return PecaCapturada;
        }

        public void desfazOMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.RetirarPeca( destino);
            p.DecrementarQntMovimentos();
            if (pecaCapturada != null)
            {
                tab.ColocarPeca(pecaCapturada, destino);
                Capturadas.Remove(pecaCapturada);
            }
            tab.ColocarPeca(p, origem);
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        { 
          Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if (EstaXeque(JogadorAtual))
            {
                desfazOMovimento(origem, destino, pecaCapturada);
                throw new ExcessaoTabuleiro("Voce nao pode se colocar em xeque!");
            }



            if (EstaXeque(Adversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            if (TesteXequeMate(Adversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                mudaJogador();
            }

        }

        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if(tab.peca(pos) == null)
            {
                throw new ExcessaoTabuleiro("Nao existe peca na posicao de origem escolhida!");
            }
            if (JogadorAtual != tab.peca(pos).Cor)
            {
                throw new ExcessaoTabuleiro("A peca de origem escolhida nao eh sua!");
            }
            if (!tab.peca(pos).ExisteMovimentosPossiveis())
            {
                throw new ExcessaoTabuleiro("Nao ha movimentos possiveis para a peca de origem!");
            }
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).MovimentoPossivel(destino))
            {
                throw new ExcessaoTabuleiro("Posicao de destino invalida!");
            }
        }

        private void mudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Capturadas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Pecas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        private Cor Adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }


        private Peca rei(Cor cor)
        {
            foreach (Peca x in PecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool EstaXeque(Cor cor)
        {
            Peca R = rei(cor);
            if (R == null)
            {
                throw new ExcessaoTabuleiro("Nao tem Rei da cor "+cor+" no tabuleiro!");
            }
            foreach (Peca x in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] mat = x.movimentoPossiveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool TesteXequeMate(Cor cor)
        {
            if (!EstaXeque(cor))
            {
                return false;
            }
            foreach (Peca x in PecasEmJogo(cor))
            {
                bool[,] mat = x.movimentoPossiveis();
                for(int i = 0; i< tab.Linhas; i++)
                {
                    for (int j = 0; j<tab.Colunas; j++)
                    {
                        if (mat[i,j])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i,j);
                            Peca pecaCapturada = ExecutaMovimento(origem, new Posicao(i, j));
                            bool testeXeque = EstaXeque(cor);
                            desfazOMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            Pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('c', 1, new Torre(Cor.Branca, tab));
            ColocarNovaPeca('d', 1, new Rei(Cor.Branca, tab));
            ColocarNovaPeca('h', 7, new Torre(Cor.Branca, tab));
           

            ColocarNovaPeca('a', 8, new Rei(Cor.Preta, tab));
            ColocarNovaPeca('b', 8, new Torre(Cor.Preta, tab));
         
        }
    }
}
