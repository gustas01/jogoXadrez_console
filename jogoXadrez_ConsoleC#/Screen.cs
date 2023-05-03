using System;
using tabuleiro;


namespace xadrez_console {
  internal class Screen {
    public static void printBoard(Board board) {
      for (int i = 0; i < board.rows; i++) {
        for (int j = 0; j < board.columns; j++) {
          if (board.Piece(i, j) == null)
            Console.Write("- ");
          Console.Write(board.Piece(i, j) + " ");
        }
        Console.WriteLine();
      }
    }
  }
}
