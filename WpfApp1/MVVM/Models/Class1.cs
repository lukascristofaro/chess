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
        bool isFirstMove = false;

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

            // Check regular forward move
            if (ChessBoard[x + direction, y] == "0")
            {
                possibleMoves.Add(new int[] { x + direction, y });
            }

            // Check capturing moves
            if (y != 7 && ChessBoard[x + direction, y + 1] != "0" && ChessBoard[x + direction, y + 1][0] - '0' != color)
            {
                possibleMoves.Add(new int[] { x + direction, y + 1 });
            }

            if (y != 0 && ChessBoard[x + direction, y - 1] != "0" && ChessBoard[x + direction, y - 1][0] - '0' != color)
            {
                possibleMoves.Add(new int[] { x + direction, y - 1 });
            }

            // Check the +1 move for the first move
            if (isFirstMove)
            {
                if (ChessBoard[x + direction * 2, y] == "0" && ChessBoard[x + direction, y] == "0")
                {
                    possibleMoves.Add(new int[] { x + direction * 2, y });
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
            int x = piecePosition[0];
            int y = piecePosition[1];
            List<int[]> possibleMoves = new List<int[]>();

            // Check possible moves in the vertical direction
            for (int i = 1; i <= 7; i++)
            {
                if (x + i < 8 && ChessBoard[x + i, y] == "0")
                {
                    possibleMoves.Add(new int[] { x + i, y });
                }
                else
                {
                    break; // Stop checking in this direction if there's an obstacle
                }
            }

            for (int i = 1; i <= 7; i++)
            {
                if (x - i >= 0 && ChessBoard[x - i, y] == "0")
                {
                    possibleMoves.Add(new int[] { x - i, y });
                }
                else
                {
                    break; // Stop checking in this direction if there's an obstacle
                }
            }

            // Check possible moves in the horizontal direction
            for (int i = 1; i <= 7; i++)
            {
                if (y + i < 8 && ChessBoard[x, y + i] == "0")
                {
                    possibleMoves.Add(new int[] { x, y + i });
                }
                else
                {
                    break; // Stop checking in this direction if there's an obstacle
                }
            }

            for (int i = 1; i <= 7; i++)
            {
                if (y - i >= 0 && ChessBoard[x, y - i] == "0")
                {
                    possibleMoves.Add(new int[] { x, y - i });
                }
                else
                {
                    break; // Stop checking in this direction if there's an obstacle
                }
            }

            // Add +1 moves in both vertical and horizontal directions
            if (x + 1 < 8 && ChessBoard[x + 1, y] == "0")
            {
                possibleMoves.Add(new int[] { x + 1, y });
            }

            if (x - 1 >= 0 && ChessBoard[x - 1, y] == "0")
            {
                possibleMoves.Add(new int[] { x - 1, y });
            }

            if (y + 1 < 8 && ChessBoard[x, y + 1] == "0")
            {
                possibleMoves.Add(new int[] { x, y + 1 });
            }

            if (y - 1 >= 0 && ChessBoard[x, y - 1] == "0")
            {
                possibleMoves.Add(new int[] { x, y - 1 });
            }

            return possibleMoves;
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
            int x = piecePosition[0];
            int y = piecePosition[1];
            List<int[]> possibleMoves = new List<int[]>();

            int[,] knightMoves = {
        {1, 2}, {1, -2}, {-1, 2}, {-1, -2},
        {2, 1}, {2, -1}, {-2, 1}, {-2, -1}
    };

            for (int i = 0; i < knightMoves.GetLength(0); i++)
            {
                int newX = x + knightMoves[i, 0];
                int newY = y + knightMoves[i, 1];

                // Check if the new position is within the bounds of the chessboard
                if (newX >= 0 && newX < 8 && newY >= 0 && newY < 8)
                {
                    // Check if the new position is empty or occupied by an opponent's piece
                    if (ChessBoard[newX, newY] == "0" || (ChessBoard[newX, newY] != "0" && ChessBoard[newX, newY][0] - '0' != color))
                    {
                        possibleMoves.Add(new int[] { newX, newY });
                    }
                }
            }

            return possibleMoves;
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
            int x = piecePosition[0];
            int y = piecePosition[1];
            List<int[]> possibleMoves = new List<int[]>();

            // Check possible moves in the diagonal directions
            for (int i = 1; i <= 7; i++)
            {
                if (x + i < 8 && y + i < 8 && ChessBoard[x + i, y + i] == "0")
                {
                    possibleMoves.Add(new int[] { x + i, y + i });
                }
                else
                {
                    break; // Stop checking in this direction if there's an obstacle
                }
            }

            for (int i = 1; i <= 7; i++)
            {
                if (x - i >= 0 && y + i < 8 && ChessBoard[x - i, y + i] == "0")
                {
                    possibleMoves.Add(new int[] { x - i, y + i });
                }
                else
                {
                    break; // Stop checking in this direction if there's an obstacle
                }
            }

            for (int i = 1; i <= 7; i++)
            {
                if (x + i < 8 && y - i >= 0 && ChessBoard[x + i, y - i] == "0")
                {
                    possibleMoves.Add(new int[] { x + i, y - i });
                }
                else
                {
                    break; // Stop checking in this direction if there's an obstacle
                }
            }

            for (int i = 1; i <= 7; i++)
            {
                if (x - i >= 0 && y - i >= 0 && ChessBoard[x - i, y - i] == "0")
                {
                    possibleMoves.Add(new int[] { x - i, y - i });
                }
                else
                {
                    break; // Stop checking in this direction if there's an obstacle
                }
            }

            return possibleMoves;
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
            int x = piecePosition[0];
            int y = piecePosition[1];
            List<int[]> possibleMoves = new List<int[]>();

            // Check possible moves in all 8 directions (horizontal, vertical, and diagonal)
            int[] dx = { 1, 1, 1, 0, 0, -1, -1, -1 };
            int[] dy = { 1, 0, -1, 1, -1, 1, 0, -1 };

            for (int i = 0; i < 8; i++)
            {
                int newX = x + dx[i];
                int newY = y + dy[i];

                // Check if the new position is within the bounds of the chessboard
                if (newX >= 0 && newX < 8 && newY >= 0 && newY < 8)
                {
                    // Check if the new position is empty or occupied by an opponent's piece
                    if (ChessBoard[newX, newY] == "0" || (ChessBoard[newX, newY] != "0" && ChessBoard[newX, newY][0] - '0' != color))
                    {
                        possibleMoves.Add(new int[] { newX, newY });
                    }
                }
            }

            return possibleMoves;
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
            int x = piecePosition[0];
            int y = piecePosition[1];
            List<int[]> possibleMoves = new List<int[]>();

            // Check possible moves in the vertical direction (similar to Rook)
            for (int i = 1; i <= 7; i++)
            {
                if (x + i < 8 && ChessBoard[x + i, y] == "0")
                {
                    possibleMoves.Add(new int[] { x + i, y });
                }
                else
                {
                    break; // Stop checking in this direction if there's an obstacle
                }
            }

            for (int i = 1; i <= 7; i++)
            {
                if (x - i >= 0 && ChessBoard[x - i, y] == "0")
                {
                    possibleMoves.Add(new int[] { x - i, y });
                }
                else
                {
                    break; // Stop checking in this direction if there's an obstacle
                }
            }

            // Check possible moves in the horizontal direction (similar to Rook)
            for (int i = 1; i <= 7; i++)
            {
                if (y + i < 8 && ChessBoard[x, y + i] == "0")
                {
                    possibleMoves.Add(new int[] { x, y + i });
                }
                else
                {
                    break; // Stop checking in this direction if there's an obstacle
                }
            }

            for (int i = 1; i <= 7; i++)
            {
                if (y - i >= 0 && ChessBoard[x, y - i] == "0")
                {
                    possibleMoves.Add(new int[] { x, y - i });
                }
                else
                {
                    break; // Stop checking in this direction if there's an obstacle
                }
            }

            // Check possible moves in the diagonal directions (similar to Bishop)
            for (int i = 1; i <= 7; i++)
            {
                if (x + i < 8 && y + i < 8 && ChessBoard[x + i, y + i] == "0")
                {
                    possibleMoves.Add(new int[] { x + i, y + i });
                }
                else
                {
                    break; // Stop checking in this direction if there's an obstacle
                }
            }

            for (int i = 1; i <= 7; i++)
            {
                if (x - i >= 0 && y + i < 8 && ChessBoard[x - i, y + i] == "0")
                {
                    possibleMoves.Add(new int[] { x - i, y + i });
                }
                else
                {
                    break; // Stop checking in this direction if there's an obstacle
                }
            }

            for (int i = 1; i <= 7; i++)
            {
                if (x + i < 8 && y - i >= 0 && ChessBoard[x + i, y - i] == "0")
                {
                    possibleMoves.Add(new int[] { x + i, y - i });
                }
                else
                {
                    break; // Stop checking in this direction if there's an obstacle
                }
            }

            for (int i = 1; i <= 7; i++)
            {
                if (x - i >= 0 && y - i >= 0 && ChessBoard[x - i, y - i] == "0")
                {
                    possibleMoves.Add(new int[] { x - i, y - i });
                }
                else
                {
                    break; // Stop checking in this direction if there's an obstacle
                }
            }

            return possibleMoves;
        }
    }
}