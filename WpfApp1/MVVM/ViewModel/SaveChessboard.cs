using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1.MVVM.ViewModel
{
    internal class SaveChessboard
    {
        public static void SaveChessPieces(string[,] chessPieces)
        {
            string filePath = GetChessPiecesFilePath();

            // Vérifier si le fichier existe
            if (File.Exists(filePath))
            {
                // Charger le contenu existant
                string existingJsonContent = File.ReadAllText(filePath);

                List<List<List<List<string>>>> chessPiecesList;

                if (string.IsNullOrWhiteSpace(existingJsonContent))
                {
                    // Si le fichier est vide, initialiser une nouvelle liste
                    chessPiecesList = new List<List<List<List<string>>>>();
                }
                else
                {
                    // Désérialiser le contenu existant en une liste de listes de listes de chaînes
                    chessPiecesList = JsonConvert.DeserializeObject<List<List<List<List<string>>>>>(existingJsonContent);
                }

                // Récupérer la dernière liste de la liste principale
                if (chessPiecesList.Count > 0)
                {
                    var lastChessBoard = chessPiecesList[chessPiecesList.Count - 1];

                    // Ajouter la liste de pièces d'échecs à la dernière liste
                    var newChessBoard = new List<List<string>>();
                    for (int i = 0; i < chessPieces.GetLength(0); i++)
                    {
                        var row = new List<string>();
                        for (int j = 0; j < chessPieces.GetLength(1); j++)
                        {
                            row.Add(chessPieces[i, j]);
                        }
                        newChessBoard.Add(row);
                    }

                    lastChessBoard.Add(newChessBoard);

                    // Sérialiser la liste mise à jour en format JSON
                    string updatedJsonContent = JsonConvert.SerializeObject(chessPiecesList, Formatting.Indented);

                    // Sauvegarder le nouveau contenu JSON dans le fichier
                    File.WriteAllText(filePath, updatedJsonContent);
                }
                else
                {
                    MessageBox.Show("La liste principale est vide.");
                }
            }
            else
            {
                MessageBox.Show("Le fichier n'existe pas.");
            }
        }


        private static string GetChessPiecesFilePath()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            string projectDirectory = directoryInfo.Parent.Parent.Parent.FullName;
            return Path.Combine(projectDirectory, "ChessPieces.json");
        }

        public static void InitChessBoard(string[,] chessBoard)
        {
            string filePath = GetChessPiecesFilePath();

            // Vérifier si le fichier existe
            if (File.Exists(filePath))
            {
                // Charger le contenu existant
                string existingJsonContent = File.ReadAllText(filePath);

                List<List<List<List<string>>>> chessPiecesList;

                if (string.IsNullOrWhiteSpace(existingJsonContent))
                {
                    // Si le fichier est vide, initialiser une nouvelle liste
                    chessPiecesList = new List<List<List<List<string>>>>();
                }
                else
                {
                    // Désérialiser le contenu existant en une liste de listes de listes de listes de chaînes
                    chessPiecesList = JsonConvert.DeserializeObject<List<List<List<List<string>>>>>(existingJsonContent);
                }

                // Ajouter une nouvelle liste à la fin du fichier JSON
                var newChessBoardList = new List<List<List<string>>>();
                var newChessBoard = new List<List<string>>();
                for (int i = 0; i < chessBoard.GetLength(0); i++)
                {
                    var row = new List<string>();
                    for (int j = 0; j < chessBoard.GetLength(1); j++)
                    {
                        row.Add(chessBoard[i, j]);
                    }

                    newChessBoard.Add(row);
                }

                newChessBoardList.Add(newChessBoard);
                chessPiecesList.Add(newChessBoardList);

                // Sérialiser la liste mise à jour en format JSON
                string updatedJsonContent = JsonConvert.SerializeObject(chessPiecesList, Formatting.Indented);

                // Sauvegarder le nouveau contenu JSON dans le fichier
                File.WriteAllText(filePath, updatedJsonContent);
            }
            else
            {
                MessageBox.Show("Le fichier n'existe pas.");
            }
        }


        public static string[,] GetChessboardAtPosition(int positionIndex, int numberParty)
        {
            string filePath = GetChessPiecesFilePath();

            // Vérifier si le fichier existe
            if (File.Exists(filePath))
            {
                // Charger le contenu existant
                string existingJsonContent = File.ReadAllText(filePath);

                // Désérialiser le contenu en une liste de listes de listes de listes de chaînes
                List<List<List<List<string>>>> chessPiecesList = JsonConvert.DeserializeObject<List<List<List<List<string>>>>>(existingJsonContent);

                if (chessPiecesList != null && numberParty >= 0 && numberParty < chessPiecesList.Count)
                {
                    // Check if positionIndex is within the valid range
                    if (positionIndex >= 0 && positionIndex < chessPiecesList[numberParty].Count)
                    {
                        // Access the innermost list and convert it to string[,]
                        List<List<string>> innerList = chessPiecesList[numberParty][positionIndex];
                        int rows = innerList.Count;
                        int columns = innerList[0].Count;

                        string[,] chessboard = new string[rows, columns];

                        for (int i = 0; i < rows; i++)
                        {
                            for (int j = 0; j < columns; j++)
                            {
                                chessboard[i, j] = innerList[i][j];
                            }
                        }

                        return chessboard;
                    }
                }
                else
                {
                    // Handle out of range error (numberParty)
                    MessageBox.Show("Party number is out of range.");
                }
            }
            else
            {
                MessageBox.Show("Le fichier n'existe pas.");
            }

            return null;
        }

        public static int GetAllChessPieces()
        {
            string filePath = GetChessPiecesFilePath();

            // Vérifier si le fichier existe
            if (File.Exists(filePath))
            {
                // Charger le contenu existant
                string existingJsonContent = File.ReadAllText(filePath);

                // Désérialiser le contenu en une liste de listes de listes de listes de chaînes
                List<List<List<List<string>>>> chessPiecesList = JsonConvert.DeserializeObject<List<List<List<List<string>>>>>(existingJsonContent);

                if (chessPiecesList != null)
                {
                    return chessPiecesList.Count - 1;
                }
            }
            else
            {
                MessageBox.Show("Le fichier n'existe pas.");
            }
            return 0;
        }

        public static int GetNumberOfParties()
        {
            string filePath = GetChessPiecesFilePath();

            // Vérifier si le fichier existe
            if (File.Exists(filePath))
            {
                // Charger le contenu existant
                string existingJsonContent = File.ReadAllText(filePath);

                // Désérialiser le contenu en une liste de listes de listes de listes de chaînes
                List<List<List<List<string>>>> chessPiecesList = JsonConvert.DeserializeObject<List<List<List<List<string>>>>>(existingJsonContent);

                if (chessPiecesList != null)
                {
                    return chessPiecesList.Count;
                }
            }
            else
            {
                MessageBox.Show("Le fichier n'existe pas.");
            }
            return 0;
        }
    }
}
