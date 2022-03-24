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
            var command = new SqlCommand(proc, Conn);
            command.CommandType = CommandType.StoredProcedure;
            var data = command.ExecuteReader();
                        
            DataTable dt = new DataTable();
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
            
        }

        private void btnAjout_Click(object sender, RoutedEventArgs e)
        { 
            
        }

        private void btnSupprime_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = DGmedoc.SelectedItem as DataRowView;
            var item = row.Row[0];
            MessageBox.Show("La ligne selectionnée est nulle", "Erreur");
            MessageBoxResult dialogResult = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette donnée ?", "Attention", MessageBoxButton.YesNo);

            if (dialogResult == MessageBoxResult.Yes)
            {
                var proc = "PS_D_Medicament";
                var command = new SqlCommand(proc, Conn);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter param1 = new SqlParameter("@MED_ID", SqlDbType.Int);
                param1.Value = item;
                command.Parameters.Add(param1);
                command.ExecuteReader();
            }
            DGReload(); // recharge la tableau au clic du bouton
        }
    }
}
