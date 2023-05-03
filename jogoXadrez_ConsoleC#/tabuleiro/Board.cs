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

    public Piece Piece(Position position) {
      return pieces[position.row, position.column];
    }

    public bool ExistPiece(Position position) {
      ValidatePosition(position);
      return Piece(position) != null;
    }

    public void InsertPiece(Piece piece, Position position) {
      if (ExistPiece(position)) {
        throw new BoardException("Já existe uma peça nessa posição"!);
      }
      pieces[position.row, position.column] = piece;
      piece.position = position;
    }

    public bool ValidPosition(Position position) {
      if (position.row < 0 || position.row >= rows || position.column < 0 || position.column >= columns)
        return false;
      return true;
    }

    public void ValidatePosition(Position position) {
      if (!ValidPosition(position))
        throw new BoardException("Posição inválida!");
    }
  }
}
