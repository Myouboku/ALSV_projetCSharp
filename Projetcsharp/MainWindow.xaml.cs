using System.Windows;
using System.Data.SqlClient;
using System;
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
        public void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            lblNomUser.Focus();
        }

        public void BtnConnexion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string reqConnect = @"Server=localhost;Database=ALSV_BDDCSHARP;User=sa;Password=info;";
                using SqlConnection connexion = new(reqConnect);

                // exécution de la PS de vérification de Login / MDP
                using SqlCommand command = new("PS_Verification_Login", connexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("UTI_MDP", lblMDP.Password);
                command.Parameters.AddWithValue("UTI_login", lblNomUser.Text);
                connexion.Open();
                var datas = command.ExecuteReader();
                datas.Read();

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
