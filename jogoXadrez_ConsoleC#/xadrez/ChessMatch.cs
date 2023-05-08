﻿using System;
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
    public bool xeque { get; private set; }

    public ChessMatch() {
      this.board = new Board(8, 8);
      turn = 1;
      currentPlayer = Color.Branca;
      isMatchFineshed = false;
      xeque = false;
      pieces = new HashSet<Piece>();
      capturedPieces = new HashSet<Piece>();
      InsertPieces();
    }

    public Piece executeMoviment(Position origin, Position destination) {
      Piece p = board.RemovePiece(origin);
      p.IncrementQteMoves();
      Piece capturedPiece = board.RemovePiece(destination);
      board.InsertPiece(p, destination);
      if (capturedPiece != null) {
        capturedPieces.Add(capturedPiece);
      }
      return capturedPiece;
    }

    public void UndoMoviment(Position origin, Position destination, Piece capturedPiece) {
      Piece piece = board.RemovePiece(destination);
      piece.DecrementQteMoves();
      if (capturedPiece != null) {
        board.InsertPiece(capturedPiece, destination);
        capturedPieces.Remove(capturedPiece);
      }
      board.InsertPiece(piece, origin);
    }

    public void MakeAPlay(Position origin, Position destination) {
      Piece capturedPiece = executeMoviment(origin, destination);
      if (IsInXeque(currentPlayer)) {
        UndoMoviment(origin, destination, capturedPiece);
        throw new BoardException("Você não pode se colcoar em xeque");
      }

      xeque = IsInXeque(Opponent(currentPlayer));

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
      foreach (Piece piece in capturedPieces) {
        if (piece.color == color) aux.Add(piece);
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

    private Color Opponent(Color color) {
      return color == Color.Branca ? Color.Preta : Color.Branca;
    }

    private Piece IsKing(Color color) {
      foreach (Piece piece in PiecesInGame(color)) {
        if (piece is King) return piece;
      }
      return null;
    }

    public bool IsInXeque(Color color) {
      Piece K = IsKing(color);
      if (K == null) throw new BoardException("Rei da cor " + color + " inexistente");

      foreach (Piece piece in PiecesInGame(Opponent(color))) {
        bool[,] mat = piece.PossibleMoviments();
        if (mat[K.position.row, K.position.column]) return true;
      }
      return false;
    }

    public void InsertNewPiece(char column, int row, Piece piece) {
      board.InsertPiece(piece, new PosicaoXadrez(column, row).toPosition());
      pieces.Add(piece);
    }

    private void InsertPieces() {
      InsertNewPiece('c', 1, new Tower(board, Color.Branca));
      InsertNewPiece('c', 2, new Tower(board, Color.Branca)); 
      InsertNewPiece('d', 1, new King(board, Color.Branca));
      InsertNewPiece('d', 2, new Tower(board, Color.Branca));
      InsertNewPiece('e', 1, new Tower(board, Color.Branca));
      InsertNewPiece('e', 2, new Tower(board, Color.Branca));
      
      InsertNewPiece('c', 7, new Tower(board, Color.Preta));
      InsertNewPiece('c', 8, new Tower(board, Color.Preta));
      InsertNewPiece('d', 8, new King(board, Color.Preta));
      InsertNewPiece('d', 7, new Tower(board, Color.Preta));
      InsertNewPiece('e', 7, new Tower(board, Color.Preta));
      InsertNewPiece('e', 8, new Tower(board, Color.Preta));

    }
  }
}
