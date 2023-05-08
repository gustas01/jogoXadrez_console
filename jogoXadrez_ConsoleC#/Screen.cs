using System;
using tabuleiro;
using xadrez;

namespace xadrez_console {
  internal class Screen {

    public static void PrintMatch(ChessMatch chessMatch) {
      printBoard(chessMatch.board);
      Console.WriteLine();
      PrintCapturedPieces(chessMatch);
      Console.WriteLine();
      Console.WriteLine("Turno: " + chessMatch.turn);
      if (!chessMatch.isMatchFineshed) {
        Console.WriteLine("Aguardando jogada da peça: " + chessMatch.currentPlayer);
        if (chessMatch.xeque) Console.WriteLine("XEQUE!!!");

      }
      else {
        Console.WriteLine("XEQUEMATE!!!");
        Console.WriteLine("Vencedor: " + chessMatch.currentPlayer);
      }

    }

    public static void PrintCapturedPieces(ChessMatch chessMatch) {
      Console.WriteLine("Peças capturadas: ");
      Console.Write("Brancas: ");
      PrintSet(chessMatch.CapturedPieces(Color.Branca));
      Console.Write("Pretas: ");
      ConsoleColor aux = Console.ForegroundColor;
      Console.ForegroundColor = ConsoleColor.Yellow;
      PrintSet(chessMatch.CapturedPieces(Color.Preta));
      Console.ForegroundColor = aux;
      Console.WriteLine();
    }

    public static void PrintSet(HashSet<Piece> set) {
      Console.Write("[");
      foreach (Piece piece in set) {
        Console.Write(piece + " ");
      }
      Console.WriteLine("]");
    }
    public static void printBoard(Board board) {
      for (int i = 0; i < board.rows; i++) {
        Console.Write(8 - i + " ");
        for (int j = 0; j < board.columns; j++) {
          printPiece(board.Piece(i, j));
        }
        Console.WriteLine();
      }
      Console.WriteLine("  a b c d e f g h");
    }


    public static void printBoard(Board board, bool[,] possiblePositions) {

      ConsoleColor orignalBackground = Console.BackgroundColor;
      ConsoleColor changedBackground = ConsoleColor.DarkGray;

      for (int i = 0; i < board.rows; i++) {
        Console.Write(8 - i + " ");
        for (int j = 0; j < board.columns; j++) {
          if (possiblePositions[i, j]) {
            Console.BackgroundColor = changedBackground;
          }
          else {
            Console.BackgroundColor = orignalBackground;
          }
          printPiece(board.Piece(i, j));
          Console.BackgroundColor = orignalBackground;
        }
        Console.WriteLine();
      }
      Console.WriteLine("  a b c d e f g h");
      Console.BackgroundColor = orignalBackground;
    }

    public static void printPiece(Piece piece) {
      if (piece == null) {
        Console.Write("- ");
      }
      else {
        if (piece.color == Color.Branca) {
          Console.Write(piece);
        }
        else {
          ConsoleColor aux = Console.ForegroundColor;
          Console.ForegroundColor = ConsoleColor.Yellow;
          Console.Write(piece);
          Console.ForegroundColor = aux;
        }
        Console.Write(" ");
      }
    }

    public static PosicaoXadrez readChessPosition() {
      string s = Console.ReadLine();
      char row = s[0];
      int column = int.Parse(s[1] + " ");
      return new PosicaoXadrez(row, column);
    }
  }
}
