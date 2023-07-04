using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xadrez_Console.Tabuleiro
{
    internal abstract class Peca
    {
        public Posicao Posicao { get; set; }

        public Cor Cor { get; protected set; }

        public int Movimentos { get; protected set; }

        public Tab tab { get; protected set; }

        public Peca() { }

        public Peca(Cor cor, Tab tab)
        {
            Posicao = null;
            Cor = cor;
            Movimentos = 0;
            this.tab = tab;
        }

        public void IncrementarQntMovimentos()
        {
            Movimentos++;
        }

        public void DecrementarQntMovimentos()
        {
            Movimentos--;
        }

        public bool ExisteMovimentosPossiveis()
        {
            bool[,] mat = movimentoPossiveis();
            for (int i = 0; i < tab.Linhas; i++)
            {
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (mat[i,j] == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool PodeMoverPara(Posicao pos)
        {
            return movimentoPossiveis()[pos.Linha, pos.Coluna];
        }

        public abstract bool[,] movimentoPossiveis();
         
    }
}
