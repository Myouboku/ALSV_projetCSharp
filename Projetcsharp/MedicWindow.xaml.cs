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
            var proc = "PS_Affichage_Medicament";
            var command = new SqlCommand(proc, Conn);
            command.CommandType = CommandType.StoredProcedure;
            var data = command.ExecuteReader();

            
            
            DataTable dt = new DataTable();
            dt.Load(data);
            DGmedoc.ItemsSource = dt.DefaultView;
        }
    }
}
