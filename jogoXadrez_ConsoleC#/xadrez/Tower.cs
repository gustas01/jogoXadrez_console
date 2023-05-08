using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez {
  internal class Tower : Piece {
    public Tower(Board board, Color color) : base(board, color) {
    }

    public override string ToString() {
      return "T";
    }

    private bool CanMove(Position position) {
      Piece piece = board.Piece(position);
      return piece == null || piece.color != color;
    }

    public override bool[,] PossibleMoviments() {
      bool[,] moviments = new bool[board.rows, board.columns];

      Position pos = new Position(0, 0);

      //acima
      pos.DefineValues(position.row - 1, position.column);
      while(board.ValidPosition(pos) && CanMove(pos)) {
        moviments[pos.row, pos.column] = true;
        if(board.Piece(pos) != null && board.Piece(pos).color != color)  {
          break;
        }
        pos.row--;
      }

      //direita
      pos.DefineValues(position.row, position.column + 1);
      while (board.ValidPosition(pos) && CanMove(pos)) {
        moviments[pos.row, pos.column] = true;
        if (board.Piece(pos) != null && board.Piece(pos).color != color) {
          break;
        }
        pos.column++;
      }


      //abaixo
      pos.DefineValues(position.row + 1, position.column);
      while (board.ValidPosition(pos) && CanMove(pos)) {
        moviments[pos.row, pos.column] = true;
        if (board.Piece(pos) != null && board.Piece(pos).color != color) {
          break;
        }
        pos.row++;
      }


      //esquerda
      pos.DefineValues(position.row, position.column - 1);
      while (board.ValidPosition(pos) && CanMove(pos)) {
        moviments[pos.row, pos.column] = true;
        if (board.Piece(pos) != null && board.Piece(pos).color != color) {
          break;
        }
        pos.column--;
      }

      return moviments;
    }
  }
}
