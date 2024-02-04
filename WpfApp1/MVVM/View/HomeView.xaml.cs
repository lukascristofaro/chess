using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System;
using chess.MVVM.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.TextFormatting;
using System.Reflection.PortableExecutable;

namespace WpfApp1.MVVM.View
{
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
            CreateChessboardButtons();
        }
        string[,] chessPieces = new string[8, 8]
            {
                {"[8,1]", "[3,1]", "[4,1]", "[6,1]", "[5,1]", "[4,1]", "[3,1]", "[8,1]"},
                {"[1,1]", "[1,1]", "[1,1]", "[1,1]", "[1,1]", "[1,1]", "[1,1]", "[1,1]"},
                {"0", "0", "0", "0", "0", "0", "0", "0"},
                {"0", "0", "0", "0", "0", "0", "0", "0"},
                {"0", "0", "0", "0", "0", "0", "0", "0"},
                {"0", "0", "0", "0", "0", "0", "0", "0"},
                {"[7,2]", "[7,2]", "[7,2]", "[7,2]", "[7,2]", "[7,2]", "[7,2]", "[7,2]"},
                {"[8,2]", "[9,2]", "[20,2]", "[22,2]", "[23,2]", "[20,2]", "[9,2]", "[8,2]"}
            };

        int[] selectedButton = new int[2];
        string selectedType = "";
        int[] selectedPiece = new int[2];
        List<int[]> pieceMoves = new List<int[]>();
        private bool isPieceSelected = false;


        private void CreateChessboardButtons()
        {
            int numRows = 8;
            int numCols = 8;

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    var button = new Button
                    {
                        Width = 50,
                        Height = 50,
                        Background = (row + col) % 2 == 0 ? Brushes.Beige : Brushes.Brown,
                        BorderBrush = Brushes.Black,
                        BorderThickness = new System.Windows.Thickness(1),
                        Content = $"{row * numCols + col + 1}"
                    };

                    Image chessPieceImage = new Image();

                    string pieceCode = chessPieces[row, col];

                    string imagePath = GetImagePath(pieceCode);

                    chessPieceImage.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                    button.Content = chessPieceImage;

                    button.Click += Button_Click;

                    Grid.SetRow(button, row);
                    Grid.SetColumn(button, col);
                    ChessGrid.Children.Add(button);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (Button b in ChessGrid.Children.OfType<Button>())
            {
                b.Background = (Grid.GetRow(b) + Grid.GetColumn(b)) % 2 == 0 ? Brushes.Beige : Brushes.Brown;
            }

            if (sender is Button button)
            {
                if (!isPieceSelected)
                {
                    isPieceSelected = true;
                    selectedButton = getPosition(button);

                    if (chessPieces[selectedButton[0], selectedButton[1]] != "0")
                    {
                        selectedPiece = getPosition(button);
                        selectedType = getPieceCode(selectedButton);

                        string pieceCode = getPieceCode(selectedButton);
                        pieceMoves = getPiece(pieceCode, chessPieces, selectedButton);

                        foreach (var move in pieceMoves)
                        {
                            int row = move[0];
                            int col = move[1];
                            Button targetButton = ChessGrid.Children
                                .OfType<Button>()
                                .FirstOrDefault(b => Grid.GetRow(b) == row && Grid.GetColumn(b) == col);

                            if (targetButton != null)
                            {
                                targetButton.Background = Brushes.Green;
                                targetButton.Click += TargetButton_Click; // Add event handler for the selected move
                            }
                        }
                    }
                }
                else
                {
                    // Check if the clicked button is one of the valid moves
                    int[] clickedPosition = getPosition(button);
                    if (pieceMoves.Contains(clickedPosition))
                    {
                        // Deselect the piece and update the chessboard
                        chessPieces[selectedButton[0], selectedButton[1]] = "0";
                        chessPieces[Grid.GetRow(button), Grid.GetColumn(button)] = selectedType;

                        UpdateChessboardButtonsContent();
                    }
                    else
                    {
                        // Handle invalid move (e.g., display a message)
                    }

                    // Remove the event handler from all buttons
                    foreach (Button b in ChessGrid.Children.OfType<Button>())
                    {
                        b.Click -= TargetButton_Click;
                    }

                    isPieceSelected = false;
                }
            }
        }


        private void TargetButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button targetButton)
            {
                int[] destination = getPosition(targetButton);

                // Check if the destination is a valid move
                if (pieceMoves.Contains(destination))
                {
                    //nothing
                }
                else
                {
                    chessPieces[destination[0], destination[1]] = selectedType;
                    chessPieces[selectedPiece[0], selectedPiece[1]] = "0";

                    // Update the chessboard
                    UpdateChessboardButtonsContent();
                }

                // Remove the event handler from all buttons
                foreach (Button b in ChessGrid.Children.OfType<Button>())
                {
                    b.Click -= TargetButton_Click;
                }
            }
        }
        private int[] getPosition(Button button)
        {
            int row = Grid.GetRow(button);
            int col = Grid.GetColumn(button);
            int[] position = new int[2] { row, col };
            return position;
        }
        private List<int[]> getPiece(string pieceCode, string[,] ChessBoard, int[] piecePosition)
        {
            Pawn blackPawn = new Pawn(piecePosition[0], piecePosition[1]);
            Pawn whitePawn = new Pawn(piecePosition[0], piecePosition[1]);
            Tower blackTower = new Tower(piecePosition[0], piecePosition[1]);
            Tower whiteTower = new Tower(piecePosition[0], piecePosition[1]);
            Knight blacKnight = new Knight(piecePosition[0], piecePosition[1]);
            Knight whiteKnight = new Knight(piecePosition[0], piecePosition[1]);
            Bishop blackBishop = new Bishop(piecePosition[0], piecePosition[1]);
            Bishop whiteBishop = new Bishop(piecePosition[0], piecePosition[1]);
            King blackKing = new King(piecePosition[0], piecePosition[1]);
            King whiteKing = new King(piecePosition[0], piecePosition[1]);
            Queen blackQueen = new Queen(piecePosition[0], piecePosition[1]);
            Queen whiteQueen = new Queen(piecePosition[0], piecePosition[1]);


            switch (pieceCode)
            {
                case "[1,1]": return blackPawn.canMove(ChessBoard, piecePosition, 0);//0 for black pieces and 1 for white pieces
                case "[7,2]": return whitePawn.canMove(ChessBoard, piecePosition, 1);
                case "[8,2]": return blackTower.canMove(ChessBoard, piecePosition, 0);
                case "[8,1]": return whiteTower.canMove(ChessBoard, piecePosition, 1);
                case "[9,2]": return blacKnight.canMove(ChessBoard, piecePosition, 0);
                case "[3,1]": return whiteKnight.canMove(ChessBoard, piecePosition, 1);
                case "[4,1]": return whiteBishop.canMove(ChessBoard, piecePosition, 1);
                case "[20,2]":return blackBishop.canMove(ChessBoard, piecePosition, 0);
                case "[6,1]":return whiteKing.canMove(ChessBoard, piecePosition, 1);
                case "[22,2]":return blackKing.canMove(ChessBoard, piecePosition, 0);
                case "[5,1]":return whiteQueen.canMove(ChessBoard, piecePosition, 1);
                case "[23,2]":return blackQueen.canMove(ChessBoard, piecePosition, 0);
                default: return new List<int[]>();
            }
        }

        private string getPieceCode(int[] piecePosition)
        {
            string pieceCode = chessPieces[piecePosition[0], piecePosition[1]];
            return pieceCode;
        }
        private string GetImagePath(string pieceCode)
        {
            switch (pieceCode)
            {
                case "[1,1]": return "/assets/white_pawn.png";
                case "[8,1]": return "/assets/white_rook.png";
                case "[3,1]": return "/assets/white_knight.png";
                case "[4,1]": return "/assets/white_bishop.png";
                case "[5,1]": return "/assets/white_queen.png";
                case "[6,1]": return "/assets/white_king.png";
                case "[7,2]": return "/assets/black_pawn.png";
                case "[8,2]": return "/assets/black_rook.png";
                case "[9,2]": return "/assets/black_knight.png";
                case "[20,2]": return "/assets/black_bishop.png";
                case "[23,2]": return "/assets/black_queen.png";
                case "[22,2]": return "/assets/black_king.png";
                default: return "";
            }
        }
        private bool isSameListe(int[] array, int[] listeInt)
        {
            if (array.Length != listeInt.Length)
            {
                return false;
            }

            for (int i = 0; i < listeInt.Length; i++)
            {
                if (listeInt[i] != array[i])
                {
                    return false;
                }
            }
            return true;
        }
        private void UpdateChessboardButtonsContent()
        {
            foreach (Button b in ChessGrid.Children.OfType<Button>())
            {
                int row = Grid.GetRow(b);
                int col = Grid.GetColumn(b);
                Image chessPieceImage = new Image();

                string pieceCode = chessPieces[row, col];
                string imagePath = GetImagePath(pieceCode);
                chessPieceImage.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                b.Content = chessPieceImage;
            }
        }
    }


}
