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
        public SqlConnection Conn { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            string reqConnect = @"Server=localhost;Database=ALSV_BDDCSHARP;User=sa;Password=info;MultipleActiveResultSets=true;";
            Conn = new(reqConnect);
            Conn.Open();
        }
        public void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            lblNomUser.Focus();
        }

        public void BtnConnexion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // exécution de la PS de vérification de Login / MDP
                using SqlCommand command = new("PS_Verification_Login", Conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("PRA_MDP", lblMDP.Password);
                command.Parameters.AddWithValue("PRA_LOGIN", lblNomUser.Text);
                
                var datas = command.ExecuteReader();
                datas.Read();

                if (datas.GetInt32(0) == 1)
                {
                    MedicWindow window = new(Conn);
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
