using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xadrez_Console.Tabuleiro
{
    internal class Posicao
    {
        public int Linha { get; set; }
        public int Coluna { get; set; }

        public Posicao() { }

        public Posicao(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }

        public void DefinirValores(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }

        public override string ToString()
        {
            return Linha+", "+Coluna;
        }
    }
}
