using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Projetcsharp
{
    /// <summary>
    /// Logique d'interaction pour MedicAjoutWindow.xaml
    /// </summary>
    public partial class MedicAjoutWindow : Window
    {
        public SqlConnection Conn { get; set; }
        public MedicAjoutWindow(SqlConnection conn)
        {
            InitializeComponent();
            Conn = conn;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BtnAjout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using SqlCommand command = new("PS_I_Medicament", Conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("MED_NOM", txtBoxNomMedic.Text);
                command.Parameters.AddWithValue("MED_PRIX", Convert.ToDouble(txtBoxPrix.Text));
                command.Parameters.AddWithValue("MED_QUANTITE", Convert.ToInt32(txtBoxQuantite.Text));
                command.Parameters.AddWithValue("MED_NB_ENTITES", Convert.ToInt32(txtBoxNbrEntite.Text));
                command.Parameters.AddWithValue("LAB_ID", Convert.ToInt32(txtBoxLab.Text));
                command.Parameters.AddWithValue("ASP_ID", Convert.ToInt32(txtBoxAspect.Text));
                command.Parameters.AddWithValue("UNI_ID", Convert.ToInt32(txtBoxUnite.Text));

                var datas = command.ExecuteReader();
                datas.Read();

                MedicWindow window = new(Conn);
                window.Show();
                Close();
            }
            catch(Exception erreur2)
            {
                MessageBox.Show("Connexion échouée :\n\n" + erreur2);
            }
        }
    }
}
