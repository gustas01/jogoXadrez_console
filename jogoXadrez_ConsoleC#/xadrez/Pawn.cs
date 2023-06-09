﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez {
  internal class Pawn : Piece {

    public Pawn(Board board, Color color) : base(board, color) { }


    public override string ToString() {
      return "P";
    }

    private bool CanMove(Position position) {
      Piece piece = board.Piece(position);
      return piece == null || piece.color != color;
    }

    private bool ExistOpponent(Position position) {
      Piece p = board.Piece(position);
      return p != null && p.color != color;
    }

    private bool Free(Position position) {
      return board.Piece(position) == null;
    }

    public override bool[,] PossibleMoviments() {
      bool[,] moviments = new bool[board.rows, board.columns];

      Position pos = new Position(0, 0);

      if (color == Color.Branca) {
        pos.DefineValues(position.row - 1, position.column);
        if (board.ValidPosition(pos) && Free(pos)) {
          moviments[pos.row, pos.column] = true;
        }
        pos.DefineValues(position.row - 2, position.column);
        Position p2 = new Position(position.row - 1, position.column);
        if (board.ValidPosition(p2) && Free(p2) && board.ValidPosition(pos) && Free(pos) && qteMoves == 0) {
          moviments[pos.row, pos.column] = true;
        }
        pos.DefineValues(position.row - 1, position.column - 1);
        if (board.ValidPosition(pos) && ExistOpponent(pos)) {
          moviments[pos.row, pos.column] = true;
        }
        pos.DefineValues(position.row - 1, position.column + 1);
        if (board.ValidPosition(pos) && ExistOpponent(pos)) {
          moviments[pos.row, pos.column] = true;
        }

      }
      else {
        pos.DefineValues(position.row + 1, position.column);
        if (board.ValidPosition(pos) && Free(pos)) {
          moviments[pos.row, pos.column] = true;
        }
        pos.DefineValues(position.row + 2, position.column);
        Position p2 = new Position(position.row + 1, position.column);
        if (board.ValidPosition(p2) && Free(p2) && board.ValidPosition(pos) && Free(pos) && qteMoves == 0) {
          moviments[pos.row, pos.column] = true;
        }
        pos.DefineValues(position.row + 1, position.column - 1);
        if (board.ValidPosition(pos) && ExistOpponent(pos)) {
          moviments[pos.row, pos.column] = true;
        }
        pos.DefineValues(position.row + 1, position.column + 1);
        if (board.ValidPosition(pos) && ExistOpponent(pos)) {
          moviments[pos.row, pos.column] = true;
        }
      }

      return moviments;
    }
  }
}
