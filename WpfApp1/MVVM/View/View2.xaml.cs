using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Newtonsoft.Json;
using System.Windows.Controls;
using System.Windows.Media;
using System.Text.Json;

namespace WpfApp1.MVVM.View
{
    public partial class View2 : UserControl
    {
        private List<List<string>> chessboardList;
        private int currentChessboardIndex = 0;

        public View2()
        {
            InitializeComponent();
            LoadChessboardsFromJson();
            DisplayChessboard();
        }

        private void LoadChessboardsFromJson()
        {
            string jsonFilePath = GetChessPiecesFilePath();
            string jsonContent = File.ReadAllText(jsonFilePath);

            
                // Utilisez JArray pour désérialiser un tableau JSON
                var jsonArray = Newtonsoft.Json.Linq.JArray.Parse(jsonContent);

                // Assurez-vous que le tableau a au moins un élément
                if (jsonArray.Count > 0)
                {
                    // Extrayez le premier élément (la liste principale) pour la désérialisation
                    chessboardList = jsonArray[0].ToObject<List<List<string>>>();
                }
                else
                {
                    MessageBox.Show("Le fichier JSON ne contient pas de données valides.", "Erreur JSON", MessageBoxButton.OK, MessageBoxImage.Error);
                }

        }



    private void NextChessboardButton_Click(object sender, RoutedEventArgs e)
        {
            currentChessboardIndex = (currentChessboardIndex + 1) % chessboardList.Count;
            DisplayChessboard();
        }

        private void PreviousChessboardButton_Click(object sender, RoutedEventArgs e)
        {
            currentChessboardIndex = (currentChessboardIndex - 1 + chessboardList.Count) % chessboardList.Count;
            DisplayChessboard();
        }
        private void DisplayChessboard()
        {
            ChessGrid.Children.Clear();

            if (chessboardList != null && chessboardList.Count > 0)
            {
                List<string> currentChessboard = chessboardList[0];

                int numRows = 8;
                int numCols = currentChessboard.Count / numRows;

                for (int row = 0; row < numRows; row++)
                {
                    for (int col = 0; col < numCols; col++)
                    {
                        var button = new Button
                        {
                            Width = 50,
                            Height = 50,
                            BorderBrush = Brushes.Black,
                            BorderThickness = new System.Windows.Thickness(1),
                            Content = currentChessboard[row * numCols + col]
                        };

                        Grid.SetRow(button, row);
                        Grid.SetColumn(button, col);
                        ChessGrid.Children.Add(button);
                    }
                }
            }
        }


        private static string GetChessPiecesFilePath()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            string projectDirectory = directoryInfo.Parent.Parent.Parent.FullName;
            return Path.Combine(projectDirectory, "ChessPieces.json");
        }
    }
}
