using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez {
  internal class King : Piece {
    public King(Board board, Color color) : base(board, color) { }

    public override string ToString() {
      return "R";
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
      if (board.ValidPosition(pos) && CanMove(pos)) {
        moviments[pos.row, pos.column] = true;
      }

      //diagonal superior direita
      pos.DefineValues(position.row - 1, position.column + 1);
      if (board.ValidPosition(pos) && CanMove(pos)) {
        moviments[pos.row, pos.column] = true;
      }

      //direita
      pos.DefineValues(position.row, position.column + 1);
      if (board.ValidPosition(pos) && CanMove(pos)) {
        moviments[pos.row, pos.column] = true;
      }

      //diagonal inferior direita
      pos.DefineValues(position.row + 1, position.column + 1);
      if (board.ValidPosition(pos) && CanMove(pos)) {
        moviments[pos.row, pos.column] = true;
      }

      //abaixo
      pos.DefineValues(position.row + 1, position.column);
      if (board.ValidPosition(pos) && CanMove(pos)) {
        moviments[pos.row, pos.column] = true;
      }

      //diagonal inferior esquerda
      pos.DefineValues(position.row + 1, position.column - 1);
      if (board.ValidPosition(pos) && CanMove(pos)) {
        moviments[pos.row, pos.column] = true;
      }

      //esquerda
      pos.DefineValues(position.row, position.column - 1);
      if (board.ValidPosition(pos) && CanMove(pos)) {
        moviments[pos.row, pos.column] = true;
      }

      //diagonal superior esquerda
      pos.DefineValues(position.row - 1, position.column - 1);
      if (board.ValidPosition(pos) && CanMove(pos)) {
        moviments[pos.row, pos.column] = true;
      }

      return moviments;
    }

  }
}
