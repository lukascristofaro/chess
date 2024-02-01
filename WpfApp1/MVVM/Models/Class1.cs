using System;

namespace chess.MVVM.Models
{
    abstract class APiece
    {
        public virtual void eat(APiece target)
        {
            this.x = target.x;
            this.y = target.y;
        }

        public abstract void canEat(APiece target);
        public abstract void canMove(int deltaX, int deltaY);

        public void move(int deltaX, int deltaY)
        {
            this.x += deltaX;
            this.y += deltaY;
        }
        public int x { get; set; }
        public int y { get; set; }
        public string name { get; protected set; }
        public string whitePath { get; protected set; }
        public string blackPath { get; protected set; }
        public string getPath(bool isWhite)
        {
            if (isWhite) return whitePath;
            else return blackPath;
        }
    }

    class Pawn : APiece
    {
        bool isFirstMove = true;

        public Pawn(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.name = "Pawn";
            this.whitePath = "/assets/white_pawn.png";
            this.blackPath = "/assets/black_pawn.png";
        }

        public override void canEat(APiece target)
        {
            if (target.x == this.x + 1 || target.x == this.x - 1)
            {
                if (target.y == this.y + 1)
                {
                    eat(target);
                }
            }
        }

        public override void canMove(int deltaX, int deltaY)
        {
            // Check if the move is valid for a pawn
            if (deltaY == 1 && deltaX == 0 && isFirstMove)
            {
                // Valid first move (move forward by 1 or 2 squares)
                isFirstMove = false;
            }
            else if ((deltaY == 1 && deltaX == 0) || (deltaY == 1 && Math.Abs(deltaX) == 1))
            {
                // Valid move (move forward by 1 square or capture diagonally)
            }
            else
            {
                // Invalid move
                throw new InvalidOperationException("Invalid move for Pawn");
            }
        }

    }

    class Tower : APiece
    {
        public Tower(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.name = "Tower";
            this.whitePath = "/assets/white_rook.png";
            this.blackPath = "/assets/black_rook.png";
            }
        
        public override void canEat(APiece target)
        {
            if (target.x == this.x || target.y == this.y)
            {
                eat(target);
            }
        }
        public override void canMove(int deltaX, int deltaY)
        {
            // Check if the move is valid for a tower (rook)
            if (deltaX == 0 || deltaY == 0)
            {
                // Valid move (move horizontally or vertically)
            }
            else
            {
                // Invalid move
                throw new InvalidOperationException("Invalid move for Tower");
            }
        }
    }
    class Knight : APiece
    {
        public Knight(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.name = "Knight";
            this.whitePath = "/assets/white_knight.png";
            this.blackPath = "/assets/black_knight.png";
        }
        public override void canEat(APiece target)
        {
            if (target.x == this.x + 1 || target.x == this.x - 1)
            {
                if (target.y == this.y + 2 || target.y == this.y - 2)
                {
                    eat(target);
                }
            }
            if (target.x == this.x + 2 || target.x == this.x - 2)
            {
                if (target.y == this.y + 1 || target.y == this.y - 1)
                {
                    eat(target);
                }
            }
        }
        public override void canMove(int deltaX, int deltaY)
        {
            // Check if the move is valid for a knight
            if ((Math.Abs(deltaX) == 1 && Math.Abs(deltaY) == 2) || (Math.Abs(deltaX) == 2 && Math.Abs(deltaY) == 1))
            {
                // Valid move (L-shaped move)
            }
            else
            {
                // Invalid move
                throw new InvalidOperationException("Invalid move for Knight");
            }
        }
    }
    class Bishop : APiece
    {
        public Bishop(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.name = "Bishop";
            this.whitePath = "/assets/white_bishop.png";
            this.blackPath = "/assets/black_bishop.png";
        }
        public override void canEat(APiece target)
        {
            if (target.x == this.x + 1 || target.x == this.x - 1)
            {
                if (target.y == this.y + 1 || target.y == this.y - 1)
                {
                    eat(target);
                }
            }
        }
        public override void canMove(int deltaX, int deltaY)
        {
            // Check if the move is valid for a bishop
            if (Math.Abs(deltaX) == Math.Abs(deltaY))
            {
                // Valid move (move diagonally)
            }
            else
            {
                // Invalid move
                throw new InvalidOperationException("Invalid move for Bishop");
            }
        }

    }
    class Queen : APiece
    {
        public Queen(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.name = "Queen";
            this.whitePath = "/assets/white_queen.png";
            this.blackPath = "/assets/black_queen.png";
        }
        public override void canEat(APiece target)
        {
            if (target.x == this.x || target.y == this.y)
            {
                eat(target);
            }
            if (target.x == this.x + 1 || target.x == this.x - 1)
            {
                if (target.y == this.y + 1 || target.y == this.y - 1)
                {
                    eat(target);
                }
            }
        }
        public override void canMove(int deltaX, int deltaY)
        {
            // Check if the move is valid for a queen
            if (Math.Abs(deltaX) == Math.Abs(deltaY) || deltaX == 0 || deltaY == 0)
            {
                // Valid move (move diagonally or horizontally/vertically)
            }
            else
            {
                // Invalid move
                throw new InvalidOperationException("Invalid move for Queen");
            }
        }

    }
    class King : APiece
    {
        public King(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.name = "King";
            this.whitePath = "/assets/white_king.png";
            this.blackPath = "/assets/black_king.png";
        }
        public override void canEat(APiece target)
        {
            if (target.x == this.x || target.y == this.y)
            {
                eat(target);
            }
            if (target.x == this.x + 1 || target.x == this.x - 1)
            {
                if (target.y == this.y + 1 || target.y == this.y - 1)
                {
                    eat(target);
                }
            }
        }
        public override void canMove(int deltaX, int deltaY)
    {
        // Check if the move is valid for a queen
        if (Math.Abs(deltaX) == Math.Abs(deltaY) || deltaX == 0 || deltaY == 0)
        {
            // Valid move (move diagonally or horizontally/vertically)
        }
        else
        {
            // Invalid move
            throw new InvalidOperationException("Invalid move for Queen");
        }
    }

    }
}