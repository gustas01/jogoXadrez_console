using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez {
  internal class King : Piece {
    private ChessMatch chessMatch;
    public King(Board board, Color color, ChessMatch chessMatch) : base(board, color) {
      this.chessMatch = chessMatch;
    }

    public override string ToString() {
      return "R";
    }

    private bool CanMove(Position position) {
      Piece piece = board.Piece(position);
      return piece == null || piece.color != color;
    }

    private bool TestTowerForRoque(Position position) {
      Piece p = board.Piece(position);
      return p != null && p is Tower && p.color == color && p.qteMoves == 0;
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

      //roque pequeno
      if(qteMoves == 0 && !chessMatch.xeque) {
        //roque pequeno
        Position towerPosition1 = new Position(position.row, position.column + 3);
        if (TestTowerForRoque(towerPosition1)) {
          Position p1 = new Position(position.row, position.column + 1);
          Position p2 = new Position(position.row, position.column + 2);
          if(board.Piece(p1) == null  && board.Piece(p2) == null) {
            moviments[position.row, position.column + 2] = true;
          }
        }
      
        //roque grande
        Position towerPosition2 = new Position(position.row, position.column - 4);
        if (TestTowerForRoque(towerPosition2)) {
          Position p1 = new Position(position.row, position.column - 1);
          Position p2 = new Position(position.row, position.column - 2);
          Position p3 = new Position(position.row, position.column - 3);
          if (board.Piece(p1) == null && board.Piece(p2) == null && board.Piece(p3) == null) {
            moviments[position.row, position.column - 2] = true;
          }
        }
      }


      return moviments;
    }

  }
}
