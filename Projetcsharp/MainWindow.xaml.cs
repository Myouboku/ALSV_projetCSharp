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
                // TODO: mettre au chargement du formulaire
                string reqConnect = @"Server=localhost;Database=ALSV_BDDCSHARP;User=sa;Password=info;";
                using SqlConnection connexion = new(reqConnect);

                using SqlCommand command = new("PS_Verification_Login", connexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("UTI_MDP", lblMDP.Text); /* getHashSha256(lblMDP.Text); */
                command.Parameters.AddWithValue("UTI_login", lblNomUser.Text);
                connexion.Open();
                var datas = command.ExecuteReader();
                datas.Read();
                MessageBox.Show(Convert.ToString(datas));

                if (datas.GetInt32(0) == 1)
                {
                    MedicWindow window = new();
                    window.Show();
                }
                else
                {
                    MessageBox.Show("Identifiant ou mot de passe incorrecte");
                }
            }
            catch (Exception erreur2)
            {
                MessageBox.Show("Connexion échouée :\n\n" + erreur2);
            }
        }
    }
}
