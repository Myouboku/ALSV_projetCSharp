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
    /// Logique d'interaction pour AjoutAvis.xaml
    /// </summary>
    public partial class AjoutAvis : Window
    {
        public SqlConnection Conn { get; set; }

        public object Praticiens { get; set; }

        public int MedID { get; set; }

        public AjoutAvis(SqlConnection conn, int item)
        {
            InitializeComponent();
            Conn = conn;
            MedID = item;
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            var proc = "PS_I_Avis";
            var command = new SqlCommand(proc, Conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            SqlParameter param1 = new("@AVI_TEXTE", SqlDbType.Text)
            {
                Value = "placeholder"
            };
            command.Parameters.Add(param1);
            SqlParameter param2 = new("@MED_ID", SqlDbType.SmallInt)
            {
                Value = MedID
            };
            command.Parameters.Add(param2);
            SqlParameter param3 = new("@PRA_ID", SqlDbType.SmallInt)
            {
                Value = userID
            };
            command.Parameters.Add(param3);
            SqlParameter param4 = new("@DATE", SqlDbType.Date)
            {
                Value = DateTime.Now
            };
            command.Parameters.Add(param4);
            command.ExecuteReader();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var proc = "PS_Affichage_Praticien";
            var command = new SqlCommand(proc, Conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                cbxPraticiens.Items.Add(reader.GetString(1) + " " + reader.GetString(2));
            }
            reader.Close();
        }
    }
}
