using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabuleiro;

namespace Xadrez_Console.Tabuleiro
{
    internal class Peca
    {
        public Posicao Posicao { get; set; }

        public Cor Cor { get; protected set; }

        public int Movimentos { get; protected set; }

        public Tab Tab { get; protected set; }

        public Peca() { }

        public Peca(Posicao posicao, Cor cor, Tab tab)
        {
            Posicao = posicao;
            Cor = cor;
            Movimentos = 0;
            Tab = tab;
        }
    }
}
