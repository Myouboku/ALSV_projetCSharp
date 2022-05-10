using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Projetcsharp
{
    /// <summary>
    /// Logique d'interaction pour AffichageAvis.xaml
    /// </summary>
    public partial class AffichageAvis : Window
    {
        public SqlConnection Conn { get; set; }

        public object MedID { get; set; }

        public List<Avis> ListeAvis { get; set; }

        public AffichageAvis(SqlConnection conn, object item)
        {
            InitializeComponent();
            Conn = conn;
            MedID = item;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            var proc = "PS_Aff_Avis";
            var command = new SqlCommand(proc, Conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            SqlParameter param = new("@MED_ID", SqlDbType.SmallInt)
            {
                Value = MedID
            };
            command.Parameters.Add(param);
            var reader = command.ExecuteReader();

            ListeAvis = new List<Avis>();
            while (reader.Read())
            {
                try
                {
                    ListeAvis.Add(new Avis
                    {
                        Commentaire = reader.GetString(0),
                        ID = reader.GetInt16(1),
                        PRA_ID = reader.GetInt16(2),
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            ltbAvis.ItemsSource = ListeAvis;
            ltbAvis.DisplayMemberPath = "Commentaire";
            reader.Close();

        }
    }
}
