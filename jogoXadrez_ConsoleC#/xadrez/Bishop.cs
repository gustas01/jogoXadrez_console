using System;
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

    private bool CanMove(Position position) {
      Piece piece = board.Piece(position);
      return piece == null || piece.color != color;
    }

    public override bool[,] PossibleMoviments() {
      bool[,] moviments = new bool[board.rows, board.columns];

      Position pos = new Position(0, 0);

      //diagonal superior direita
      pos.DefineValues(position.row - 1, position.column + 1);
      while (board.ValidPosition(pos) && CanMove(pos)) {
        moviments[pos.row, pos.column] = true;
        if (board.Piece(pos) != null && board.Piece(pos).color != color) {
          break;
        }
        pos.DefineValues(pos.row - 1, pos.column + 1);
      }

      //diagonal inferior direita
      pos.DefineValues(position.row + 1, position.column + 1);
      while (board.ValidPosition(pos) && CanMove(pos)) {
        moviments[pos.row, pos.column] = true;
        if (board.Piece(pos) != null && board.Piece(pos).color != color) {
          break;
        }
        pos.DefineValues(pos.row + 1, pos.column + 1);
      }


      //diagonal inferior esquerda
      pos.DefineValues(position.row + 1, position.column - 1);
      while (board.ValidPosition(pos) && CanMove(pos)) {
        moviments[pos.row, pos.column] = true;
        if (board.Piece(pos) != null && board.Piece(pos).color != color) {
          break;
        }
        pos.DefineValues(pos.row + 1, pos.column - 1);
      }


      //diagonal superior esquerda
      pos.DefineValues(position.row - 1, position.column - 1);
      while (board.ValidPosition(pos) && CanMove(pos)) {
        moviments[pos.row, pos.column] = true;
        if (board.Piece(pos) != null && board.Piece(pos).color != color) {
          break;
        }
        pos.DefineValues(pos.row - 1, pos.column - 1);
      }

      return moviments;
    }
  }
}
