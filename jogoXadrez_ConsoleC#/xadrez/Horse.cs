﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez {
  internal class Horse : Piece {
    public Horse(Board board, Color color) : base(board, color) {
    }

    public override bool[,] PossibleMoviments() {
      throw new NotImplementedException();
    }

    public override string ToString() {
      return "C";
    }
  }
}
