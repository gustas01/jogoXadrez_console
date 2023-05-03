using tabuleiro;
using xadrez_console;
using xadrez;

Board board = new Board(8, 8);

board.InsertPiece(new King(board, Color.Branca), new Position(0, 0));
board.InsertPiece(new Tower(board, Color.Branca), new Position(3, 7));

Screen.printBoard(board);
