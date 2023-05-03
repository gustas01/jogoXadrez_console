using tabuleiro;
using xadrez_console;
using xadrez;

try {
  Board board = new Board(8, 8);
  board.InsertPiece(new King(board, Color.Branca), new Position(0, 0));
  board.InsertPiece(new Tower(board, Color.Branca), new Position(0, 1));

  Screen.printBoard(board);
}catch(BoardException e) {
    Console.WriteLine(e.Message);
}

