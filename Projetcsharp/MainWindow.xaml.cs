using System.Windows;
using System.Data.SqlClient;
using System;
using System.Diagnostics;
using XSystem.Security.Cryptography;
using System.Text;
using System.Data;

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

        // hash les MDP en sha256
        public static string getHashSha256(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }

        private void btnConnexion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string reqConnect = @"Server=localhost;Database=ALSV_BDDCSHARP;User=sa;Password=info;";
                SqlConnection connexion;
                connexion = new SqlConnection(reqConnect);
                connexion.Open();

                string nomUtilisateur = lblNomUser.Text;
                string mdp = getHashSha256(lblMDP.Text);

                if (true)
                {
                    MedicWindow window = new();
                    window.Show();
                }
                else
                {
                    MessageBox.Show("Identifiant ou mot de passe incorrecte");
                }

                // MessageBox.Show("Connexion réussie");
                connexion.Close();
            }
            catch (Exception erreur2)
            {
                MessageBox.Show("Connexion échouée :\n\n" + erreur2);
            }
        }
    }
}
