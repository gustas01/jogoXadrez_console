using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez {
  internal class ChessMatch {
    public Board board { get; private set; }
    private int turn;
    private Color currentPlayer;
    public bool isMatchFineshed { get; private set; }

    public ChessMatch() {
      this.board = new Board(8, 8);
      turn = 1;
      currentPlayer = Color.White;
      isMatchFineshed = false;
      InsertPieces();
    }

    public void executeMoviment(Position origin, Position destination) {
      Piece p = board.RemovePiece(origin);
      p.incrementQteMoves();
      Piece capturedPiece = board.RemovePiece(destination);
      board.InsertPiece(p, destination);
    }

    private void InsertPieces() {
      board.InsertPiece(new King(board, Color.White), new PosicaoXadrez('c', 1).toPosition());
      board.InsertPiece(new Tower(board, Color.Black), new PosicaoXadrez('a', 5).toPosition());
    }
  }
}
