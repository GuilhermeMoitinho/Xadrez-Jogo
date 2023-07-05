using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Xadrez_Console.Tabuleiro;

namespace Xadrez_Console.Xadrez
{
    internal class Peao : Peca
    {

        public Peao() { }

        public Peao(Cor cor, Tab tab) : base(cor, tab)
        {

        }

        public override string ToString()
        {
            return "P";
        }

        private bool existeInimigo(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p != null && p.Cor != Cor;
        }

        private bool livre(Posicao pos)
        {
            return tab.peca(pos) == null;
        }

        public override bool[,] movimentoPossiveis()
        {
            bool[,] mat = new bool[tab.Linhas, tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            if (Cor == Cor.Branca)
            {
                //acima
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
                if (tab.PosicaoValida(pos) && livre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                //nordeste
                pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                if (tab.PosicaoValida(pos) && livre(pos) && Movimentos == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                //direita
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (tab.PosicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                //Sudeste
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (tab.PosicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
            }
            else
            {

                //abaixo
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (tab.PosicaoValida(pos) && livre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                //Sudoeste
                pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                if (tab.PosicaoValida(pos) && existeInimigo(pos) && Movimentos == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                //Esquerda
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (tab.PosicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                //Noroeste
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (tab.PosicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
            }

            return mat;
        }
    }
}
