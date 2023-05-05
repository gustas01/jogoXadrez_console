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

    public void incrementQteMoves() { this.qteMoves++; }

    public abstract bool[,] PossibleMoviments();
  }
}