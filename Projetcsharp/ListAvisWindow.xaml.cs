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
    /// Logique d'interaction pour ListAvisWindow.xaml
    /// </summary>
    public partial class ListAvisWindow : Window
    {
        public SqlConnection Conn { get; set; }

        public object MedicId { get; set; }
        public ListAvisWindow(System.Data.SqlClient.SqlConnection conn, object item)
        {
            InitializeComponent();
            Conn = conn;
            MedicId = item;
        }

        private void AvisLoad()
        {
            dgListAvis.Items.Refresh();

            DataTable dt = new();
            using SqlCommand comdAvis = new("PS_Aff_Avis", Conn);
            comdAvis.CommandType = CommandType.StoredProcedure;
            comdAvis.Parameters.AddWithValue("MED_ID", MedicId);
            var datas = comdAvis.ExecuteReader();

            List<Avis> maListAvis = new List<Avis>();
            while (datas.Read())
            {
                maListAvis.Add(new Avis
                {
                    ID = (Int16)datas["AVI_ID"],
                    Text = (string)datas["avi_texte"],
                    Nom = (string)datas["Nom"]
                });
            }
            dgListAvis.ItemsSource = maListAvis;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void dgLoad(object sender, RoutedEventArgs e)
        {
            AvisLoad();
        }
    }
}
