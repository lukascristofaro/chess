using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System;
using chess.MVVM.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;


namespace WpfApp1.MVVM.View
{
    public partial class HomeView : UserControl
    {
        private APiece selectedPiece;
        List<APiece> whitePieces = new List<APiece>();
        List<APiece> blackPieces = new List<APiece>();
        public HomeView()
        {
            InitializeComponent();
            CreateChessboardButtons();
        }

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
                    };
                    Grid.SetRow(button, row);
                    Grid.SetColumn(button, col);
                    this.ChessGrid.Children.Add(button);
                    button.Click += Button_Click;
                    
                }
            }

            definePiece();
            placePieces();

            void definePiece()
            {
                for (int i = 0; i < numCols; i++)
                {
                    this.whitePieces.Add(new Pawn(i, 6));
                    this.blackPieces.Add(new Pawn(i, 1));
                    
                }
                this.blackPieces.Add(new Tower(0, 0));
                this.blackPieces.Add(new Tower(7, 0));
                this.whitePieces.Add(new Tower(7, 7));
                this.whitePieces.Add(new Tower(0, 7));
                this.whitePieces.Add(new Knight(1, 7));
                this.whitePieces.Add(new Knight(6, 7));
                this.blackPieces.Add(new Knight(1, 0));
                this.blackPieces.Add(new Knight(6, 0));
                this.whitePieces.Add(new Bishop(2, 7));
                this.whitePieces.Add(new Bishop(5, 7));
                this.blackPieces.Add(new Bishop(2, 0));
                this.blackPieces.Add(new Bishop(5, 0));
                this.whitePieces.Add(new Queen(3, 7));
                this.blackPieces.Add(new Queen(3, 0));
                this.whitePieces.Add(new King(4, 7));
                this.blackPieces.Add(new King(4, 0));
            }

            void placePieces()
            {
                foreach (APiece piece in whitePieces)
                {
                    Image chessPieceImage = new Image();
                    chessPieceImage.Source = new BitmapImage(new Uri(piece.getPath(true), UriKind.Relative));
                    Button button = new Button();
                    button.Content = chessPieceImage;
                    button.Click += Button_Click;
                    Grid.SetRow(button, piece.y);
                    Grid.SetColumn(button, piece.x);
                    ChessGrid.Children.Add(button);
                }

                foreach (APiece piece in blackPieces)
                {
                    Image chessPieceImage = new Image();
                    chessPieceImage.Source = new BitmapImage(new Uri(piece.getPath(false), UriKind.Relative));
                    Button button = new Button();
                    button.Content = chessPieceImage;
                    button.Click += Button_Click;
                    Grid.SetRow(button, piece.y);
                    Grid.SetColumn(button, piece.x);
                    ChessGrid.Children.Add(button);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                int row = Grid.GetRow(button);
                int col = Grid.GetColumn(button);
                if (selectedPiece == null)
                {
                    APiece clickedPiece = GetPieceAtPosition(row, col);
                    if (clickedPiece != null)
                    {
                        selectedPiece = clickedPiece;
                        MessageBox.Show($"Piece selected: {selectedPiece.name}");
                    }
                }
                else
                {
                    selectedPiece.canMove(col - selectedPiece.x, row - selectedPiece.y);
                    Grid.SetRow(button, selectedPiece.y);
                    Grid.SetColumn(button, selectedPiece.x);
                    selectedPiece = null;
                }
            }
        }

        private void Button_RightClick(object sender, MouseButtonEventArgs e)
        {
            selectedPiece = null;
        }

        private APiece GetPieceAtPosition(int row, int col)
        {
            return whitePieces.Concat(blackPieces).FirstOrDefault(piece => piece.x == col && piece.y == row);
        }
    }
}
