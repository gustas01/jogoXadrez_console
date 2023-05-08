using tabuleiro;
using xadrez_console;
using xadrez;

try {
  ChessMatch chessMatch = new ChessMatch();
  while (!chessMatch.isMatchFineshed) {
    try {

    Console.Clear();
    Screen.printBoard(chessMatch.board);
    Console.WriteLine();
    Console.WriteLine("Turno: " + chessMatch.turn);
    Console.WriteLine("Aguardando jogada da peça: " + chessMatch.currentPlayer);

    Console.Write("Origem: ");
    Position origin = Screen.readChessPosition().toPosition();
    chessMatch.ValidateOriginPosition(origin);

    bool[,] possiblePositions = chessMatch.board.Piece(origin).PossibleMoviments();
    Console.Clear();
    Screen.printBoard(chessMatch.board, possiblePositions);

    Console.WriteLine();
    Console.Write("Destino: ");
    Position destination = Screen.readChessPosition().toPosition();
      chessMatch.ValidateDestinationPosition(origin, destination);

    chessMatch.MakeAPlay(origin, destination);
    }
      catch(BoardException e) {
      Console.WriteLine(e.Message);
      Console.ReadLine();
    }
  }
}
catch (BoardException e) {
  Console.WriteLine(e.Message);
}

