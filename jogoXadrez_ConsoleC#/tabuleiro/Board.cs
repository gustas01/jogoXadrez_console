using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tabuleiro {
  internal class Board {
    public int rows;
    public int columns;
    private Piece[,] pieces;

    public Board(int rows, int columns) {
      this.rows = rows;
      this.columns = columns;
      pieces = new Piece[rows, columns];
    }

    public Piece Piece(int row, int column) {
      return pieces[row, column];
    }

    public void InsertPiece(Piece piece, Position position) {
      pieces[position.row, position.column] = piece;
      piece.position = position;
    }
  }
}
