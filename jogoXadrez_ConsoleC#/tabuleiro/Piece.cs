using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tabuleiro {
  abstract class Piece {
    public Position position;
    public Color color { get; protected set; }
    public int qteMoves { get; protected set; }
    public Board board { get; protected set; }

    public Piece(Board board, Color color) {
      this.position = null;
      this.board = board;
      this.color = color;
      this.qteMoves = 0;
    }

    public void IncrementQteMoves() { this.qteMoves++; }
    public void DecrementQteMoves() { this.qteMoves--; }

    public bool ExistPossibleMoviments() {
      bool[,] moviments = PossibleMoviments();
      for (int i = 0; i < board.rows; i++) {
        for(int j = 0; j < board.columns; j++) {
          if (moviments[i, j]) return true;
        }
      }
      return false;
    }

    public bool PossibleMoviment(Position position) {
      return PossibleMoviments()[position.row, position.column];
    }

    public abstract bool[,] PossibleMoviments();
  }
}