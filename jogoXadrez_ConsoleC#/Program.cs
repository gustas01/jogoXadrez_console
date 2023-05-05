﻿using tabuleiro;
using xadrez_console;
using xadrez;

try {
  ChessMatch chessMatch = new ChessMatch();
  while (!chessMatch.isMatchFineshed) {
    Console.Clear();
    Screen.printBoard(chessMatch.board);

    Console.Write("Origem: ");
    Position origin = Screen.readChessPosition().toPosition();

    bool[,] possiblePositions = chessMatch.board.Piece(origin).PossibleMoviments();
    Console.Clear();
    Screen.printBoard(chessMatch.board, possiblePositions);

    Console.WriteLine();
    Console.Write("Destino: ");
    Position destinarion = Screen.readChessPosition().toPosition();

    chessMatch.executeMoviment(origin, destinarion);
  }
}
catch (BoardException e) {
  Console.WriteLine(e.Message);
}

