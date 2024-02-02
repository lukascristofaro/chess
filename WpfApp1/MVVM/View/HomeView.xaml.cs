using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System;
using chess.MVVM.Models;
using System.Collections.Generic;
using System.Linq;
//alexis

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
                {"[2,1]", "[3,1]", "[4,1]", "[5,1]", "[6,1]", "[4,1]", "[3,1]", "[2,1]"},
                {"[1,1]", "[1,1]", "[1,1]", "[1,1]", "[1,1]", "[1,1]", "[1,1]", "[1,1]"},
                {"0", "0", "0", "0", "0", "0", "0", "0"},
                {"0", "0", "0", "0", "0", "0", "0", "0"},
                {"0", "0", "0", "0", "0", "0", "0", "0"},
                {"0", "0", "0", "0", "0", "0", "0", "0"},
                {"[7,2]", "[7,2]", "[7,2]", "[7,2]", "[7,2]", "[7,2]", "[7,2]", "[7,2]"},
                {"[8,2]", "[9,2]", "[10,2]", "[11,2]", "[12,2]", "[10,2]", "[9,2]", "[8,2]"}
            };
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
            if (sender is Button button)
            {
                
                int[] piecePosition = getPosition(button);
                string pieceCode = getPieceCode(piecePosition);
                List<int[]> pieceMoves = getPiece(pieceCode, chessPieces, piecePosition);

                foreach (var move in pieceMoves)
                {
                    int row = move[0];
                    int col = move[1];
                    Button targetButton = ChessGrid.Children
                        .OfType<Button>()
                        .First(b => Grid.GetRow(b) == row && Grid.GetColumn(b) == col);

                    targetButton.Background = Brushes.Green; 
                }
                string movesString = string.Join(Environment.NewLine, pieceMoves.Select(move => $"[{move[0]}, {move[1]}]"));
                MessageBox.Show($"Possible Moves:{Environment.NewLine}{movesString}");

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
           Pawn pawn = new Pawn(piecePosition[0], piecePosition[1]);  
            switch (pieceCode)
            {
                case "[1,1]": return pawn.canMove(ChessBoard, piecePosition);
                case "[7,2]": return pawn.canMove(ChessBoard, piecePosition);
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
                case "[2,1]": return "/assets/white_rook.png";
                case "[3,1]": return "/assets/white_knight.png";
                case "[4,1]": return "/assets/white_bishop.png";
                case "[5,1]": return "/assets/white_queen.png";
                case "[6,1]": return "/assets/white_king.png";
                case "[7,2]": return "/assets/black_pawn.png";
                case "[8,2]": return "/assets/black_rook.png";
                case "[9,2]": return "/assets/black_knight.png";
                case "[10,2]": return "/assets/black_bishop.png";
                case "[11,2]": return "/assets/black_queen.png";
                case "[12,2]": return "/assets/black_king.png";
                default: return "";
            }
        }
    }
}
