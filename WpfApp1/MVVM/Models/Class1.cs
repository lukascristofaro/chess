using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Media3D;

namespace chess.MVVM.Models
{
    abstract class APiece
    {
        public abstract List<int[]> canMove(string[,] ChessBoard, int[] piecePosition, int color);
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
        public override List<int[]> canMove(string[,] ChessBoard, int[] piecePosition, int color)
        {
            int x = piecePosition[0];
            int y = piecePosition[1];
            List<int[]> possibleMoves = new List<int[]>();
            int direction = 1;
            if (color == 1)
            {
                direction = -1;
            }


            if (isFirstMove)
            {
                if (ChessBoard[x + direction*2, y ] == "0")
                {
                    possibleMoves.Add(new int[] { x + direction*2, y});
                }
                if (y != 7)
                {
                    if (ChessBoard[x + direction * 2, y + 1] != "0")
                    {
                        possibleMoves.Add(new int[] { x + direction * 2, y + 1 });
                    }
                }

                if (y != 0)
                {
                    if (ChessBoard[x + direction * 2, y - 1] != "0")
                    {
                        possibleMoves.Add(new int[] { x - direction * 2, y + 1 });
                    }
                }
                isFirstMove = false;

            }
            if (ChessBoard[x + direction, y] == "0")
            {
                possibleMoves.Add(new int[] { x + direction, y });
            }
            if ( y != 7) 
            {
                if (ChessBoard[x + direction, y + 1] != "0")
                {
                    possibleMoves.Add(new int[] { x + direction, y + 1 });
                }
            }
            if (y != 0)
            {
                if (ChessBoard[x + direction, y - 1] != "0")
                {
                    possibleMoves.Add(new int[] { x - direction, y + 1 });
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

        public override List<int[]> canMove(string[,] ChessBoard, int[] piecePosition, int color)
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

        public override List<int[]> canMove(string[,] ChessBoard, int[] piecePosition, int color)
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

        public override List<int[]> canMove(string[,] ChessBoard, int[] piecePosition, int color)
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

        public override List<int[]> canMove(string[,] ChessBoard, int[] piecePosition, int color)
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

        public override List<int[]> canMove(string[,] ChessBoard, int[] piecePosition, int color)
        {
            throw new NotImplementedException();
        }
    }
}