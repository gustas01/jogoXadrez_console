using System;
using tabuleiro;

namespace xadrez {
  internal class PosicaoXadrez {
    public char column;
    public int row;

    public PosicaoXadrez(char column, int row) {
      this.column = column;
      this.row = row;
    }

    public Position toPosition() {
      return new Position(8 - row, column - 'a');
    }

    public override string ToString() {
      return "" + column + row;
    }
  }
}
