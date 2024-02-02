using System;
using System.Collections.Generic;

namespace chess.MVVM.Models
{
    abstract class APiece
    {
        public abstract List<int[]> canMove(string[,] ChessBoard, int[] piecePosition);
        public void move(int deltaX, int deltaY)
        {
            this.x += deltaX;
            this.y += deltaY;
        }
        public int x { get; set; }
        public int y { get; set; }
    }

    class Pawn : APiece
    {
        bool isFirstMove = true;

        public Pawn(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public override List<int[]> canMove(string[,] ChessBoard, int[] piecePosition)
        {
            int x = piecePosition[0];
            int y = piecePosition[1];
            List<int[]> possibleMoves = new List<int[]>();

            bool isTargetPositionEmpty = x >= 0 && x < ChessBoard.GetLength(0) && y + 1 >= 0 && y + 1 < ChessBoard.GetLength(1);
            bool isDifferentColor = isTargetPositionEmpty && ChessBoard[x, y + 1] != "0" || ChessBoard[x, y + 1][0] != ChessBoard[piecePosition[0], piecePosition[1]][0];

            if (isFirstMove)
            {
                if (ChessBoard[x, y + 1] == "0" || (y + 2 < ChessBoard.GetLength(1) && ChessBoard[x, y + 2] == "0" && isDifferentColor))
                {
                    possibleMoves.Add(new int[] { x, y + 1 });
                }
                if (isDifferentColor && ChessBoard[x + 1, y + 1] != "0")
                {
                    possibleMoves.Add(new int[] { x + 1, y + 1 });
                }
                if (isDifferentColor && ChessBoard[x - 1, y + 1] != "0")
                {
                    possibleMoves.Add(new int[] { x - 1, y + 1 });
                }
            }
            else
            {
                if (isDifferentColor && ChessBoard[x, y + 1] == "0")
                {
                    possibleMoves.Add(new int[] { x, y + 1 });
                }
                if (isDifferentColor && ChessBoard[x + 1, y + 1] != "0")
                {
                    possibleMoves.Add(new int[] { x + 1, y + 1 });
                }
                if (isDifferentColor && ChessBoard[x - 1, y + 1] != "0")
                {
                    possibleMoves.Add(new int[] { x - 1, y + 1 });
                }
            }
            return possibleMoves;
        }
    }
    class Tower : APiece
    {
        public Tower(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override List<int[]> canMove(string[,] ChessBoard, int[] piecePosition)
        {
            throw new NotImplementedException();
        }
    }
    class Knight : APiece
    {
        public Knight(int x, int y)
        {
            this.x = x;
            this.y = y;
            
        }

        public override List<int[]> canMove(string[,] ChessBoard, int[] piecePosition)
        {
            throw new NotImplementedException();
        }
    }
    class Bishop : APiece
    {
        public Bishop(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override List<int[]> canMove(string[,] ChessBoard, int[] piecePosition)
        {
            throw new NotImplementedException();
        }
    }
    class Queen : APiece
    {
        public Queen(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override List<int[]> canMove(string[,] ChessBoard, int[] piecePosition)
        {
            throw new NotImplementedException();
        }
    }
    class King : APiece
    {
        public King(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override List<int[]> canMove(string[,] ChessBoard, int[] piecePosition)
        {
            throw new NotImplementedException();
        }
    }
}