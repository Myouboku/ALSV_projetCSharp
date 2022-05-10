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
    /// Logique d'interaction pour AjoutAvisWindow.xaml
    /// </summary>
    public partial class AjoutAvisWindow : Window
    {
        public SqlConnection Conn { get; set; }
        public AjoutAvisWindow(System.Data.SqlClient.SqlConnection conn)
        {
            InitializeComponent();
            Conn = conn;
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            //Praticien
            List<Combobox> listPra = new List<Combobox>();
            var ProcPra = "PS_Affichage_Praticien_nomUniq_Concat";
            var cmdPra = new SqlCommand(ProcPra, Conn);
            SqlDataReader drPra = cmdPra.ExecuteReader();
            while (drPra.Read())
            {
                listPra.Add(new Combobox
                {
                    Id = (Int16)drPra["Id"],
                    Nom = (string)drPra["Nom"]
                });
            }
            cbListePraticien.ItemsSource = listPra;
            cbListePraticien.DisplayMemberPath = "Nom";


            //Médicament
            List<Combobox> listMed = new List<Combobox>();
            var ProcMed = "PS_Affichage_Medicament";
            var cmdMed = new SqlCommand(ProcMed, Conn);
            SqlDataReader drMed = cmdMed.ExecuteReader();
            while (drMed.Read())
            {
                listMed.Add(new Combobox
                {
                    Id = (Int16)drMed["ID"],
                    Nom = (string)drMed["Nom Médicament"]
                });
            }
            cbboxListMedoc.ItemsSource = listMed;
            cbboxListMedoc.DisplayMemberPath = "Nom";
        }

        private void btnAddAvis_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using SqlCommand command = new("PS_I_Avis", Conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("MED_ID ", ((Combobox)cbboxListMedoc.SelectedItem).Id);
                command.Parameters.AddWithValue("PRA_ID ", ((Combobox)cbListePraticien.SelectedItem).Id);
                command.Parameters.AddWithValue("AVI_TEXTE", txtbAvis.Text);

                var datas = command.ExecuteReader();
                datas.Read();

                MedicWindow window = new(Conn);
                window.ShowDialog();
                Close();
            }
            catch (Exception erreur3)
            {
                MessageBox.Show("Connexion échouée :\n\n" + erreur3);
            }
        }
    }
}
