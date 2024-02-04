using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfApp1.MVVM.ViewModel;

namespace WpfApp1.MVVM.View
{
    public partial class View2 : UserControl
    {
        private string[,] chessPieces = new string[8, 8];
        private int currentPositionIndex = 0;
        private int maxPosition = SaveChessboard.GetAllChessPieces();
        private int numberOfParties = SaveChessboard.GetNumberOfParties();

        public List<PartyItem> Parties { get; set; } = new List<PartyItem>();
        public int CurrentPartyIndex { get; set; } = 0;

        public View2()
        {
            InitializeComponent();

            for (int i = 0; i < numberOfParties; i++)
            {
                Parties.Add(new PartyItem { PartyIndex = i, PartyName = $"Party {i + 1}" });
            }

            DataContext = this;

            PartyComboBox.SelectionChanged += PartyComboBox_SelectionChanged;

            if (maxPosition > 0)
            {
                chessPieces = SaveChessboard.GetChessboardAtPosition(currentPositionIndex, CurrentPartyIndex);
                ShowChessboard();
            }
        }

        public class PartyItem
        {
            public int PartyIndex { get; set; }
            public string PartyName { get; set; }
        }

        private void BtnAvant_Click(object sender, RoutedEventArgs e)
        {
            if (currentPositionIndex > 0)
            {
                currentPositionIndex--;
                ShowChessboard();
            }
        }

        private void BtnApres_Click(object sender, RoutedEventArgs e)
        {
            if (currentPositionIndex < maxPosition)
            {
                currentPositionIndex++;
                ShowChessboard();
            }
        }

        private void PartyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle party change here
            CurrentPartyIndex = PartyComboBox.SelectedIndex;
            currentPositionIndex = 0;
            ShowChessboard();
        }

        private void ShowChessboard()
        {
            chessPieces = SaveChessboard.GetChessboardAtPosition(currentPositionIndex, CurrentPartyIndex);
            if (chessPieces != null)
            {
                // Clear the existing buttons in ChessGrid
                ChessGrid.Children.Clear();

                int numRows = 8;
                int numCols = 8;

                for (int row = 0; row < numRows; row++)
                {
                    for (int col = 0; col < numCols; col++)
                    {
                        var button = new Button
                        {
                            Width = 40,
                            Height = 40,
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

                        Grid.SetRow(button, row);
                        Grid.SetColumn(button, col);
                        ChessGrid.Children.Add(button);
                    }
                }
            }
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

