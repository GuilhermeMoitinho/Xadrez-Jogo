using Xadrez_Console.Tabuleiro;
using Xadrez_Console.Xadrez;

internal class Peao : Peca
{
    private PartidaDeXadrez partida;

    public Peao() { }

    public Peao(Cor cor, Tab tab, PartidaDeXadrez partida) : base(cor, tab)
    {
        this.partida = partida;
    }

    public override string ToString()
    {
        return "P";
    }

    private bool existeInimigo(Posicao pos)
    {
        Peca p = tab.peca(pos);
        return p != null && p.Cor != this.Cor;
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
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (tab.PosicaoValida(pos) && existeInimigo(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            //noroeste
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (tab.PosicaoValida(pos) && existeInimigo(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            //2 casas acima (movimento inicial)
            if (Movimentos == 0)
            {
                Posicao pos2 = new Posicao(Posicao.Linha - 2, Posicao.Coluna);
                if (tab.PosicaoValida(pos2) && livre(pos2))
                {
                    mat[pos2.Linha, pos2.Coluna] = true;
                }
            }
            //JOGADA ESPECIAL PASSANT
            if (Posicao.Linha == 3)
            {
                Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                if (tab.PosicaoValida(esquerda) && existeInimigo(esquerda) && tab.peca(esquerda) == partida.VulneravelEnPassant)
                {
                    mat[esquerda.Linha - 1, esquerda.Coluna] = true;
                }
                Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                if (tab.PosicaoValida(direita) && existeInimigo(direita) && tab.peca(direita) == partida.VulneravelEnPassant)
                {
                    mat[direita.Linha - 1, direita.Coluna] = true;
                }
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

            //sudoeste
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (tab.PosicaoValida(pos) && existeInimigo(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            //sudeste
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (tab.PosicaoValida(pos) && existeInimigo(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            //2 casas abaixo (movimento inicial)
            if (Movimentos == 0)
            {
                Posicao pos2 = new Posicao(Posicao.Linha + 2, Posicao.Coluna);
                if (tab.PosicaoValida(pos2) && livre(pos2))
                {
                    mat[pos2.Linha, pos2.Coluna] = true;
                }
            }
            if (Posicao.Linha == 4)
            {
                Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                if (tab.PosicaoValida(esquerda) && existeInimigo(esquerda) && tab.peca(esquerda) == partida.VulneravelEnPassant)
                {
                    mat[esquerda.Linha + 1, esquerda.Coluna] = true;
                }
                Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                if (tab.PosicaoValida(direita) && existeInimigo(direita) && tab.peca(direita) == partida.VulneravelEnPassant)
                {
                    mat[direita.Linha + 1, direita.Coluna] = true;
                }
            }
        }

        return mat;
    }
}
