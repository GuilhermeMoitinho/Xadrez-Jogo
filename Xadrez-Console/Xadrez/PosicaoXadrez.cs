using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez_Console.Tabuleiro;


namespace Xadrez_Console.Xadrez
{
    internal class PosicaoXadrez
    {

        public int Linha { get; set; }
        public char Coluna { get; set; }

        public PosicaoXadrez() { }

        public PosicaoXadrez(char coluna, int linha)
        {
            Linha = linha;
            Coluna = coluna;
        }

        public Posicao toPosicao()
        {
            return new Posicao(8 - Linha, Coluna - 'a');
        }

        public override string ToString()
        {
            return ""+ Coluna + Linha;
        }
    }
}
