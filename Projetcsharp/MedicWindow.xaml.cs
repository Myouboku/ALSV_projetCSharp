using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows;

namespace Projetcsharp
{
    /// <summary>
    /// Logique d'interaction pour MedicWindow.xaml
    /// </summary>
    public partial class MedicWindow : Window
    {
        public SqlConnection Conn { get; set; }

        public string username { get; set; }

        public int userID { get; set; }

        public string avis { get; set; }

        /// <summary>
        /// Charge ou recharge le tableau de données (médicaments)
        /// </summary>
        public void DGReload()
        {
            DGmedoc.Items.Refresh();

            var proc = "PS_Affichage_Medicament";
            var command = new SqlCommand(proc, Conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            var data = command.ExecuteReader();
                        
            DataTable dt = new();
            dt.Load(data);
            DGmedoc.ItemsSource = dt.DefaultView;
            DGmedoc.Columns[0].Visibility = Visibility.Hidden;
        }

        public MedicWindow(SqlConnection conn, string username)
        {
            InitializeComponent();
            this.username = username;
            Conn = conn;
        }

        private void btnDeconnect_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            
        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var proc = "PS_Recuperation_IdPraticien";
            var command = new SqlCommand(proc, Conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            SqlParameter param = new("@PRA_LOGIN", SqlDbType.VarChar)
            {
                Value = username
            };
            command.Parameters.Add(param);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                userID = reader.GetInt16(0);
            }
            reader.Close();

            DGReload(); // charge le tableau au chargement
        }

        private void btnModification_Click(object sender, RoutedEventArgs e)
        {


            if (DGmedoc.SelectedItem is not DataRowView row)
                MessageBox.Show("Veuillez séléctionner une ligne", "Erreur");
            else
            {

                var item = row.Row[0];
                MedicModifWindow window = new(Conn, item);
                window.Show();
            }
        }


        private void btnAjout_Click(object sender, RoutedEventArgs e)
        {
            MedicAjoutWindow window = new(Conn);
            window.ShowDialog();
        }

        private void btnSupprime_Click(object sender, RoutedEventArgs e)
        {
            if (DGmedoc.SelectedItem is not DataRowView row)

                MessageBox.Show("Veuillez séléctionner une ligne", "Erreur");

            else
            {
                var item = row.Row[0];
                MessageBoxResult dialogResult = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette donnée ?", "Attention", MessageBoxButton.YesNo);

                if (dialogResult == MessageBoxResult.Yes)
                {
                    var proc = "PS_D_Medicament";
                    var command = new SqlCommand(proc, Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    SqlParameter param1 = new("@MED_ID", SqlDbType.Int)
                    {
                        Value = item
                    };
                    command.Parameters.Add(param1);
                    command.ExecuteReader();
                }
                DGReload(); // recharge la tableau au clic du bouton
            }
        }


        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void btnVoirAvis_Click(object sender, RoutedEventArgs e)
        {
            if (DGmedoc.SelectedItem is not DataRowView row)
                MessageBox.Show("Veuillez selectionner une ligne", "Erreur");
            else
            {
                var item = row.Row[0];

                AffichageAvis window = new(Conn, item);
                window.ShowDialog();

                DGReload(); // recharge la tableau au clic du boutton
            }
        }

        private void btnAjoutAvis_Click(object sender, RoutedEventArgs e)
        {
            if (DGmedoc.SelectedItem is not DataRowView row)
                MessageBox.Show("Veuillez selectionner une ligne", "Erreur");
            else
            {
                var item = row.Row[0];

                AjoutAvis window = new(Conn, item);
                window.ShowDialog();

                DGReload(); // recharge la tableau au clic du boutton
            }

        private void btnListePraticien_Click(object sender, RoutedEventArgs e)
        {
            PraticienWindow window = new(Conn);
            window.Show();

        }
    }
}
