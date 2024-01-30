using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1.MVVM.View
{
    public partial class HomeView : UserControl
    {
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
                        Background = (row + col) % 2 == 0 ? Brushes.White : Brushes.Black,
                        BorderBrush = Brushes.Black,
                        BorderThickness = new System.Windows.Thickness(1),
                        Content = $"{row * numCols + col + 1}" // Utilisation d'un identifiant unique pour chaque bouton
                                                               // Vous pouvez personnaliser le contenu du bouton
                    };

                    // Ajouter un gestionnaire d'événements pour le clic sur chaque bouton
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
                // Gérer l'événement de clic sur un bouton
                int row = Grid.GetRow(button);
                int col = Grid.GetColumn(button);

                // Faites quelque chose avec les coordonnées (par exemple, affichez-les)
                MessageBox.Show($"Bouton cliqué : Ligne {row}, Colonne {col}");
            }
        }
    }
}
