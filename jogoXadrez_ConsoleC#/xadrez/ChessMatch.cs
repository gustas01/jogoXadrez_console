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
    private HashSet<Piece> pieces;
    private HashSet<Piece> capturedPieces;

    public ChessMatch() {
      this.board = new Board(8, 8);
      turn = 1;
      currentPlayer = Color.Branca;
      isMatchFineshed = false;
      pieces = new HashSet<Piece>();
      capturedPieces = new HashSet<Piece>();
      InsertPieces();
    }

    public void executeMoviment(Position origin, Position destination) {
      Piece p = board.RemovePiece(origin);
      p.incrementQteMoves();
      Piece capturedPiece = board.RemovePiece(destination);
      board.InsertPiece(p, destination);
      if(capturedPiece != null) {
        capturedPieces.Add(capturedPiece);
      }
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

    public HashSet<Piece> CapturedPieces(Color color) {
      HashSet<Piece> aux = new HashSet<Piece>();
      foreach(Piece piece in capturedPieces) {
        if(piece.color == color) aux.Add(piece);
      }
      return aux;
    }

    public HashSet<Piece> PiecesInGame(Color color) {
      HashSet<Piece> aux = new HashSet<Piece>();
      foreach (Piece piece in pieces) {
        if (piece.color == color) aux.Add(piece);
      }
      aux.ExceptWith(CapturedPieces(color));
      return aux;
    }

    public void InsertNewPiece(char column, int row, Piece piece) {
      board.InsertPiece(piece, new PosicaoXadrez(column, row).toPosition());
      pieces.Add(piece);
    }

    private void InsertPieces() {
      InsertNewPiece('c', 1, new King(board, Color.Branca));
      InsertNewPiece('b', 2, new King(board, Color.Branca));
      InsertNewPiece('b', 1, new King(board, Color.Branca));
      InsertNewPiece('c', 2, new King(board, Color.Branca));
      InsertNewPiece('d', 1, new King(board, Color.Branca));
      InsertNewPiece('d', 2, new King(board, Color.Branca));
      InsertNewPiece('a', 5, new Tower(board, Color.Preta));

    }
  }
}
