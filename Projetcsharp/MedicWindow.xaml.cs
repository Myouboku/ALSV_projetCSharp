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

        public MedicWindow(SqlConnection conn)
        {
            InitializeComponent();
            Conn = conn;
        }

        private void btnDeconnect_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DGReload(); // charge le tableeau au chargement
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
            window.Show();

        }

        private void btnSupprime_Click(object sender, RoutedEventArgs e)
        {
            if (DGmedoc.SelectedItem is not DataRowView row)
                MessageBox.Show("La ligne selectionnée est nulle", "Erreur");
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

        private void btnListePraticien_Click(object sender, RoutedEventArgs e)
        {
            PraticienWindow window = new(Conn);
            window.ShowDialog();
        }
    }
}
