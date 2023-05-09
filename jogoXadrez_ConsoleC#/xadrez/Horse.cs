using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez {
  internal class Horse : Piece {
    public Horse(Board board, Color color) : base(board, color) {
    }

    public override string ToString() {
      return "C";
    }

    private bool CanMove(Position position) {
      Piece piece = board.Piece(position);
      return piece == null || piece.color != color;
    }

    public override bool[,] PossibleMoviments() {
      bool[,] moviments = new bool[board.rows, board.columns];

      Position pos = new Position(0, 0);

      pos.DefineValues(position.row - 1, position.column - 2);
      if (board.ValidPosition(pos) && CanMove(pos)) {
        moviments[pos.row, pos.column] = true;
      }

      pos.DefineValues(position.row - 2, position.column - 1);
      if (board.ValidPosition(pos) && CanMove(pos)) {
        moviments[pos.row, pos.column] = true;
      }


      pos.DefineValues(position.row - 2, position.column + 1);
      if (board.ValidPosition(pos) && CanMove(pos)) {
        moviments[pos.row, pos.column] = true;
      }


      pos.DefineValues(position.row - 1, position.column + 2);
      if (board.ValidPosition(pos) && CanMove(pos)) {
        moviments[pos.row, pos.column] = true;
      }


      pos.DefineValues(position.row + 1, position.column + 2);
      if (board.ValidPosition(pos) && CanMove(pos)) {
        moviments[pos.row, pos.column] = true;
      }

      pos.DefineValues(position.row + 2, position.column + 1);
      if (board.ValidPosition(pos) && CanMove(pos)) {
        moviments[pos.row, pos.column] = true;
      }

      pos.DefineValues(position.row + 2, position.column - 1);
      if (board.ValidPosition(pos) && CanMove(pos)) {
        moviments[pos.row, pos.column] = true;
      }

      pos.DefineValues(position.row + 1, position.column - 2);
      if (board.ValidPosition(pos) && CanMove(pos)) {
        moviments[pos.row, pos.column] = true;
      }

      return moviments;
    }
  }
}
