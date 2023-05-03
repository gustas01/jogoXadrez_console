﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez {
  internal class Queen : Piece {
    public Queen(Board board, Color color) : base(board, color) {
    }

    public override string ToString() {
      return "D";
    }
  }
}
