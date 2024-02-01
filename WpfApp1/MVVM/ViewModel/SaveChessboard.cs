using Newtonsoft.Json;
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
            // Convert the array to JSON
            string jsonContent = JsonConvert.SerializeObject(chessPieces, Formatting.Indented);

            MessageBox.Show(jsonContent);

            // Save the JSON to a file in the project directory
            DirectoryInfo directoryInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            string projectDirectory = directoryInfo.Parent.Parent.Parent.FullName;
            string filePath = Path.Combine(projectDirectory, "ChessPieces.json");

            File.WriteAllText(filePath, jsonContent);
        }
    }
}
