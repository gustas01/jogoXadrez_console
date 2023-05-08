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

    public void ValidateOriginPosition(Position position) {
      if (board.Piece(position) == null) throw new BoardException("Não existe peça na posição de origem escolhida");
      if (currentPlayer != board.Piece(position).color) throw new BoardException("A peça escolhida não é sua");
      if (!board.Piece(position).ExistPossibleMoviments()) throw new BoardException("Não há movimentos possíveis para a peça escolhida");
    }

    public void ValidateDestinationPosition(Position origin, Position destination) {
      if (!board.Piece(origin).CanMoveTo(destination)) throw new BoardException("Posição de destino inválida");
    }

    private void InsertPieces() {
      board.InsertPiece(new King(board, Color.Branca), new PosicaoXadrez('c', 1).toPosition());
      board.InsertPiece(new King(board, Color.Branca), new PosicaoXadrez('b', 1).toPosition());
      board.InsertPiece(new King(board, Color.Branca), new PosicaoXadrez('b', 2).toPosition());
      board.InsertPiece(new King(board, Color.Branca), new PosicaoXadrez('c', 2).toPosition());
      board.InsertPiece(new King(board, Color.Branca), new PosicaoXadrez('d', 1).toPosition());
      board.InsertPiece(new King(board, Color.Branca), new PosicaoXadrez('d', 2).toPosition());
      board.InsertPiece(new Tower(board, Color.Preta), new PosicaoXadrez('a', 5).toPosition());

    }
  }
}
