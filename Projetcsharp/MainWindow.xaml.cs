using System.Windows;
using System.Data.SqlClient;
using System;

namespace Projetcsharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnConnexion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string reqConnect = @"Server=localhost;Database=ALSV_BDDCSHARP;User=sa;Password=info;";
                SqlConnection connexion;

                connexion = new SqlConnection(reqConnect);
                connexion.Open();
                MessageBox.Show("Connexion réussie");
                connexion.Close();
                MedicWindow window = new();
                window.Show();
            }
            catch (Exception erreur)
            {
                MessageBox.Show("Connexion échouée :\n\n" + erreur);
            }
        }
    }
}
