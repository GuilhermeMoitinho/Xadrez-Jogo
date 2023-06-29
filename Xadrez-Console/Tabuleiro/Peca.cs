﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabuleiro;

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

        public abstract bool[,] movimentoPossiveis();
         
    }
}
