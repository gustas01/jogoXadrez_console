using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tabuleiro {
  internal class Posicao {
    public int row;
    public int column;

    public Posicao(int row, int column) {
      this.row = row;
      this.column = column;
    }

    public override string ToString() {
      return row + ", " + column;
    }
  }
}
