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
            // Get the existing JSON content (if any)
            string filePath = GetChessPiecesFilePath();
            string existingJsonContent = "";
            List<string[,]> chessPiecesList;

            if (File.Exists(filePath))
            {
                existingJsonContent = File.ReadAllText(filePath);
                // Désérialiser le contenu existant en une liste d'arrays de chaînes
                chessPiecesList = JsonConvert.DeserializeObject<List<string[,]>>(existingJsonContent);
            }
            else
            {
                // Si le fichier n'existe pas, créer une nouvelle liste
                chessPiecesList = new List<string[,]>();
            }

            // Ajouter chessPieces à la liste existante
            chessPiecesList.Add(chessPieces);

            // Sérialiser la liste mise à jour en format JSON
            string updatedJsonContent = JsonConvert.SerializeObject(chessPiecesList, Formatting.Indented);

            // Sauvegarder le nouveau contenu JSON dans le fichier
            File.WriteAllText(filePath, updatedJsonContent);
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

                // Désérialiser le contenu existant en une liste d'arrays de chaînes
                List<string[,]> chessPiecesList = JsonConvert.DeserializeObject<List<string[,]>>(existingJsonContent);

                // Ajouter le contenu de chessBoard à la liste existante
                if (chessPiecesList == null)
                {
                    chessPiecesList = new List<string[,]>();
                }
                chessPiecesList.Add(chessBoard);

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
    }
}
