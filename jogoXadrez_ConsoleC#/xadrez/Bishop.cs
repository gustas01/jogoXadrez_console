﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez {
  internal class Bishop : Piece {
    public Bishop(Board board, Color color) : base(board, color) {
    }

    public override string ToString() {
      return "B";
    }
  }
}
