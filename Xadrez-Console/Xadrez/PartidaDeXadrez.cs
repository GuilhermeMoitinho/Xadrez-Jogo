﻿using System;
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
        public Peca VulneravelEnPassant { get;private set; }



        public PartidaDeXadrez()
        {
            tab = new Tab(8,8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Xeque = false;
            VulneravelEnPassant = null;
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

            //JOGADA ESPECIAL ROQUEPEQUENO
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca t = tab.RetirarPeca(origemT);
                t.IncrementarQntMovimentos();
                tab.ColocarPeca(t, destinoT);
            }

            //JOGADA ESPECIAL ROQUEGRANDE
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = tab.RetirarPeca(origemT);
                T.IncrementarQntMovimentos();
                tab.ColocarPeca(T, destinoT);
            }

            //JOGADA ESPECIAL PASSANT
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && PecaCapturada == null)
                {
                    Posicao posP;
                    if (p.Cor == Cor.Branca)
                    {
                        posP = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.Linha - 1, destino.Coluna);
                    }
                    PecaCapturada = tab.RetirarPeca(posP);
                    Capturadas.Add(PecaCapturada);
                }
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

            //Roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca t = tab.RetirarPeca(destinoT);
                t.DecrementarQntMovimentos();
                tab.ColocarPeca(t, origemT);
            }
            //Roque Grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = tab.RetirarPeca(destinoT);
                T.DecrementarQntMovimentos();
                tab.ColocarPeca(T, origemT);
            }

            //JOGADA ESPECIAL PASSANT
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant)
                {
                    Peca peao = tab.RetirarPeca(destino);
                    Posicao posP;
                    if (p.Cor == Cor.Branca)
                    {
                        posP = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.Coluna);
                    }
                    tab.ColocarPeca(peao, posP);
                }
            }

        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        { 
          Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if (EstaXeque(JogadorAtual))
            {
                desfazOMovimento(origem, destino, pecaCapturada);
                throw new ExcessaoTabuleiro("Voce nao pode se colocar em xeque!");
            }
            Peca p = tab.peca(destino);

            //Jogada especial Promocao
            if (p is Peao)
            {
                if (p.Cor == Cor.Branca && destino.Linha == 0 || (p.Cor == Cor.Preta  && destino.Linha == 7) )
                {
                    p = tab.RetirarPeca(destino);
                    Pecas.Remove(p);
                    Peca dama = new Dama(p.Cor, tab);
                    tab.ColocarPeca(dama, destino);
                    Pecas.Add(dama);
                }
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

           
            //JOGADA ESPECIAL PASSANT
            if (p is Peao && (destino.Linha == origem.Linha - 2 ||  destino.Linha == origem.Linha + 2))
            {
                VulneravelEnPassant = p;
            }
            else
            {
                VulneravelEnPassant = null;
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



            ColocarNovaPeca('a', 1, new Torre(Cor.Branca, tab));
            ColocarNovaPeca('b', 1, new Cavalo(Cor.Branca, tab));
            ColocarNovaPeca('c', 1, new Bispo(Cor.Branca, tab));
            ColocarNovaPeca('d', 1, new Dama(Cor.Branca, tab));
            ColocarNovaPeca('e', 1, new Rei(Cor.Branca, tab, this));
            ColocarNovaPeca('f', 1, new Bispo(Cor.Branca, tab));
            ColocarNovaPeca('g', 1, new Cavalo(Cor.Branca, tab));
            ColocarNovaPeca('h', 1, new Torre(Cor.Branca, tab));
            ColocarNovaPeca('a', 2, new Peao(Cor.Branca, tab, this));
            ColocarNovaPeca('b', 2, new Peao(Cor.Branca, tab, this));
            ColocarNovaPeca('c', 2, new Peao(Cor.Branca, tab, this));
            ColocarNovaPeca('d', 2, new Peao(Cor.Branca, tab, this));
            ColocarNovaPeca('e', 2, new Peao(Cor.Branca, tab, this));
            ColocarNovaPeca('f', 2, new Peao(Cor.Branca, tab, this));
            ColocarNovaPeca('g', 2, new Peao(Cor.Branca, tab, this));
            ColocarNovaPeca('h', 2, new Peao(Cor.Branca, tab, this));

            ColocarNovaPeca('a', 8, new Torre(Cor.Preta, tab));
            ColocarNovaPeca('b', 8, new Cavalo(Cor.Preta, tab));
            ColocarNovaPeca('c', 8, new Bispo(Cor.Preta, tab));
            ColocarNovaPeca('d', 8, new Dama(Cor.Preta, tab));
            ColocarNovaPeca('e', 8, new Rei(Cor.Preta, tab, this));
            ColocarNovaPeca('f', 8, new Bispo(Cor.Preta, tab));
            ColocarNovaPeca('g', 8, new Cavalo(Cor.Preta, tab));
            ColocarNovaPeca('h', 8, new Torre(Cor.Preta, tab));
            ColocarNovaPeca('a', 7, new Peao(Cor.Preta, tab, this));
            ColocarNovaPeca('b', 7, new Peao(Cor.Preta, tab, this));
            ColocarNovaPeca('c', 7, new Peao(Cor.Preta, tab, this));
            ColocarNovaPeca('d', 7, new Peao(Cor.Preta, tab, this));
            ColocarNovaPeca('e', 7, new Peao(Cor.Preta, tab, this));
            ColocarNovaPeca('f', 7, new Peao(Cor.Preta, tab, this));
            ColocarNovaPeca('g', 7, new Peao(Cor.Preta, tab, this));
            ColocarNovaPeca('h', 7, new Peao(Cor.Preta, tab, this));


        }
    }
}
