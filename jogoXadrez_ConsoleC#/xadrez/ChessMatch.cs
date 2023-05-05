using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez {
  internal class ChessMatch {
    public Board board { get; private set; }
    public int turn { get; private set; }
    public Color currentPlayer { get; private set; }
    public bool isMatchFineshed { get; private set; }

    public ChessMatch() {
      this.board = new Board(8, 8);
      turn = 1;
      currentPlayer = Color.Branca;
      isMatchFineshed = false;
      InsertPieces();
    }

    public void executeMoviment(Position origin, Position destination) {
      Piece p = board.RemovePiece(origin);
      p.incrementQteMoves();
      Piece capturedPiece = board.RemovePiece(destination);
      board.InsertPiece(p, destination);
    }

    public void MakeAPlay(Position origin, Position destination) {
      executeMoviment(origin, destination);
      turn++;
      currentPlayer = currentPlayer == Color.Branca ? Color.Preta : Color.Branca;
    }

    private void InsertPieces() {
      board.InsertPiece(new King(board, Color.Branca), new PosicaoXadrez('c', 1).toPosition());
      board.InsertPiece(new Tower(board, Color.Preta), new PosicaoXadrez('a', 5).toPosition());
    }
  }
}
