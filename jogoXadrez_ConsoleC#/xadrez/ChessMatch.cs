using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
    public Piece VulnerableEnPassant { get; private set; }

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

    public Piece ExecuteMoviment(Position origin, Position destination) {
      Piece p = board.RemovePiece(origin);
      p.IncrementQteMoves();
      Piece capturedPiece = board.RemovePiece(destination);
      board.InsertPiece(p, destination);
      if (capturedPiece != null) {
        capturedPieces.Add(capturedPiece);
      }

      //roque pequeno
      if(p is King && destination.column == origin.column + 2) {
        Position originTower = new Position(origin.row, origin.column + 3);
        Position destinationTower = new Position(origin.row, origin.column + 1);
        Piece tower = board.RemovePiece(originTower);
        tower.IncrementQteMoves();
        board.InsertPiece(tower, destinationTower);
      }

      //roque grande
      if (p is King && destination.column == origin.column - 2) {
        Position originTower = new Position(origin.row, origin.column - 4);
        Position destinationTower = new Position(origin.row, origin.column - 1);
        Piece tower = board.RemovePiece(originTower);
        tower.IncrementQteMoves();
        board.InsertPiece(tower, destinationTower);
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

      //roque pequeno
      if (piece is King && destination.column == origin.column + 2) {
        Position originTower = new Position(origin.row, origin.column + 3);
        Position destinationTower = new Position(origin.row, origin.column + 1);
        Piece tower = board.RemovePiece(destinationTower);
        tower.DecrementQteMoves();
        board.InsertPiece(tower, originTower);
      }

      //roque grande
      if (piece is King && destination.column == origin.column - 2) {
        Position originTower = new Position(origin.row, origin.column - 4);
        Position destinationTower = new Position(origin.row, origin.column - 1);
        Piece tower = board.RemovePiece(destinationTower);
        tower.DecrementQteMoves();
        board.InsertPiece(tower, originTower);
      }
    }

    public void MakeAPlay(Position origin, Position destination) {
      Piece capturedPiece = ExecuteMoviment(origin, destination);
      if (IsInXeque(currentPlayer)) {
        UndoMoviment(origin, destination, capturedPiece);
        throw new BoardException("Você não pode se colcoar em xeque");
      }

      xeque = IsInXeque(Opponent(currentPlayer));

      if (XequeMateTest(Opponent(currentPlayer))) isMatchFineshed = true;
      else {
        turn++;
        currentPlayer = currentPlayer == Color.Branca ? Color.Preta : Color.Branca;
      }

    }

    public void ValidateOriginPosition(Position position) {
      if (board.Piece(position) == null) throw new BoardException("Não existe peça na posição de origem escolhida");
      if (currentPlayer != board.Piece(position).color) throw new BoardException("A peça escolhida não é sua");
      if (!board.Piece(position).ExistPossibleMoviments()) throw new BoardException("Não há movimentos possíveis para a peça escolhida");
    }

    public void ValidateDestinationPosition(Position origin, Position destination) {
      if (!board.Piece(origin).PossibleMoviment(destination)) throw new BoardException("Posição de destino inválida");
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

    public bool XequeMateTest(Color color) {
      if (!IsInXeque(color)) return false;

      foreach (Piece piece in PiecesInGame(color)) {
        bool[,] mat = piece.PossibleMoviments();
        for (int i = 0; i < board.rows; i++) {
          for (int j = 0; j < board.columns; j++) {
            if (mat[i, j]) {
              Position origin = piece.position;
              Position destination = new Position(i, j);
              Piece capturedPiece = ExecuteMoviment(origin, destination);
              bool xequeTest = IsInXeque(color);
              UndoMoviment(origin, destination, capturedPiece);
              if (!xequeTest) {
                return false;
              }
            }
          }
        }
      }
      return true;
    }

    public void InsertNewPiece(char column, int row, Piece piece) {
      board.InsertPiece(piece, new PosicaoXadrez(column, row).toPosition());
      pieces.Add(piece);
    }

    private void InsertPieces() {
      InsertNewPiece('a', 1, new Tower(board, Color.Branca));
      InsertNewPiece('b', 1, new Horse(board, Color.Branca));
      InsertNewPiece('c', 1, new Bishop(board, Color.Branca));
      InsertNewPiece('d', 1, new Queen(board, Color.Branca));
      InsertNewPiece('e', 1, new King(board, Color.Branca, this));
      InsertNewPiece('f', 1, new Bishop(board, Color.Branca));
      InsertNewPiece('g', 1, new Horse(board, Color.Branca));
      InsertNewPiece('h', 1, new Tower(board, Color.Branca));
      InsertNewPiece('a', 2, new Pawn(board, Color.Branca, this));
      InsertNewPiece('b', 2, new Pawn(board, Color.Branca, this));
      InsertNewPiece('c', 2, new Pawn(board, Color.Branca, this));
      InsertNewPiece('d', 2, new Pawn(board, Color.Branca, this));
      InsertNewPiece('e', 2, new Pawn(board, Color.Branca, this));
      InsertNewPiece('f', 2, new Pawn(board, Color.Branca, this));
      InsertNewPiece('g', 2, new Pawn(board, Color.Branca, this));
      InsertNewPiece('h', 2, new Pawn(board, Color.Branca, this));

      InsertNewPiece('a', 8, new Tower(board, Color.Preta));
      InsertNewPiece('b', 8, new Horse(board, Color.Preta));
      InsertNewPiece('c', 8, new Bishop(board, Color.Preta));
      InsertNewPiece('d', 8, new Queen(board, Color.Preta));
      InsertNewPiece('e', 8, new King(board, Color.Preta, this));
      InsertNewPiece('f', 8, new Bishop(board, Color.Preta));
      InsertNewPiece('g', 8, new Horse(board, Color.Preta));
      InsertNewPiece('h', 8, new Tower(board, Color.Preta));
      InsertNewPiece('a', 7, new Pawn(board, Color.Preta, this));
      InsertNewPiece('b', 7, new Pawn(board, Color.Preta, this));
      InsertNewPiece('c', 7, new Pawn(board, Color.Preta, this));
      InsertNewPiece('d', 7, new Pawn(board, Color.Preta, this));
      InsertNewPiece('e', 7, new Pawn(board, Color.Preta, this));
      InsertNewPiece('f', 7, new Pawn(board, Color.Preta, this));
      InsertNewPiece('g', 7, new Pawn(board, Color.Preta, this));
      InsertNewPiece('h', 7, new Pawn(board, Color.Preta, this));

    }
  }
}
