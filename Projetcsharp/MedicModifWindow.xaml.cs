using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Data.SqlClient;
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
    /// Logique d'interaction pour MedicModifWindow.xaml
    /// </summary>
    public partial class MedicModifWindow : Window
    {

        public SqlConnection Conn { get; set; }

        public object MedicId { get; set; } 


        public MedicModifWindow(SqlConnection conn, object item)
        {
            InitializeComponent();
            Conn = conn;
            MedicId = item;
        }

       

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            using SqlCommand comdMedic = new("PS_Recuperation_MedicamentId", Conn);
            comdMedic.CommandType = CommandType.StoredProcedure;
            comdMedic.Parameters.AddWithValue("MED_ID", MedicId);
            var datas = comdMedic.ExecuteReader();
            datas.Read();
            txtBoxNom.Text = datas[1].ToString();
            txtBoxPrix.Text = datas["MED_PRIX"].ToString();
            textBoxQuantite.Text = datas["MED_QUANTITE"].ToString();

            //Laboratoire
            List<Combobox> listLab = new List<Combobox>();
            var ProcLab = "PS_Recuperation_Laboratoires";
            var cmdLab = new SqlCommand(ProcLab, Conn);
            SqlDataReader drLab = cmdLab.ExecuteReader();
            while (drLab.Read())
            {
                listLab.Add(new Combobox
                {
                    Id = (Int16)drLab["LAB_ID"],
                    Nom = (string)drLab["LAB_NOM"]
                });

            }
            comboboxLab.ItemsSource = listLab;
            
            comboboxLab.DisplayMemberPath = "Nom";
            comboboxLab.SelectedIndex = (Int16)datas["LAB_ID"] ; 

            //Aspect
            List<Combobox> listAsp = new List<Combobox>();
            var procAsp = "PS_Recuperation_Aspects";
            var cmdAsp = new SqlCommand(procAsp, Conn);
            SqlDataReader drAsp = cmdAsp.ExecuteReader();
            while (drAsp.Read())
            {
                listAsp.Add(new Combobox
                {
                    Id = (Int16)drAsp["ASP_ID"],
                    Nom = (string)drAsp["ASP_LIBELLE"]
                });

            }
            comboboxAsp.ItemsSource = listAsp;
            comboboxAsp.DisplayMemberPath = "Nom";
            comboboxAsp.SelectedIndex = (Int16)datas["ASP_ID"];



            //Unité
            List<Combobox> listUni = new List<Combobox>();
            var ProcUni = "PS_Recuperation_Unites";
            var cmdUni = new SqlCommand(ProcUni, Conn);
            SqlDataReader drUni = cmdUni.ExecuteReader();
            while (drUni.Read())
            {
                listUni.Add(new Combobox
                {
                    Id = (Int16)drUni["UNI_ID"],
                    Nom = (string)drUni["UNI_LIBELLE"]
                });
            }
            comboboxUni.ItemsSource = listUni;
            comboboxUni.DisplayMemberPath = "Nom";
            comboboxUni.SelectedIndex = (Int16)datas["UNI_ID"];

            //Contre indication
            List<Combobox> listCont = new List<Combobox>();
            var ProcCont = "PS_Recuperation_ContreIndications";
            var cmdCont = new SqlCommand(ProcCont, Conn);
            SqlDataReader drCont = cmdCont.ExecuteReader();
            while (drCont.Read())
            {
                listCont.Add(new Combobox
                {
                    Id = (Int16)drCont["CON_ID"],
                    Nom = (string)drCont["CON_LIBELLE"]
                });
            }
            comboboxCon.ItemsSource = listCont;
            comboboxCon.DisplayMemberPath = "Nom";
            comboboxCon.SelectedIndex = (Int16)datas["CON_ID"];

            //Effet Secondaire
            List<Combobox> listEffSecon = new List<Combobox>();
            var procEffSecon = "PS_Recuperation_EffetsSecondaires";
            var cmdEffSecond = new SqlCommand(procEffSecon, Conn);
            SqlDataReader drEffSecond = cmdEffSecond.ExecuteReader();
            while (drEffSecond.Read())
            {
                listEffSecon.Add(new Combobox
                {
                    Id = (Int16)drEffSecond["EFF_ID"],
                    Nom = (string)drEffSecond["EFF_LIBELLE"]
                });
            }
            comboBoxeeffSec.ItemsSource = listEffSecon;
            comboBoxeeffSec.DisplayMemberPath = "Nom";
            comboBoxeeffSec.SelectedIndex = (Int16)datas["EFF_ID"];

            

        }

        private void BtnModif_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                using SqlCommand command = new("PS_E_Medicament", Conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("MED_ID", MedicId);
                command.Parameters.AddWithValue("MED_NOM", txtBoxNom.Text);
                command.Parameters.AddWithValue("MED_PRIX", Convert.ToDouble(txtBoxPrix.Text));
                command.Parameters.AddWithValue("MED_QUANTITE", Convert.ToInt32(textBoxQuantite.Text));
                command.Parameters.AddWithValue("MED_NB_ENTITES", Convert.ToInt32(txtBoxNbEntite.Text));
                command.Parameters.AddWithValue("ASP_ID", ((Combobox)comboboxAsp.SelectedItem).Id);
                command.Parameters.AddWithValue("EFF_ID", ((Combobox)comboBoxeeffSec.SelectedItem).Id); 
                command.Parameters.AddWithValue("LAB_ID", ((Combobox)comboboxLab.SelectedItem).Id);
                command.Parameters.AddWithValue("UNI_ID", ((Combobox)comboboxUni.SelectedItem).Id);
                command.Parameters.AddWithValue("CON_ID", ((Combobox)comboboxCon.SelectedItem).Id);
                var datas = command.ExecuteReader();
                datas.Read();

                MedicWindow window = new(Conn);
                window.Show();
                Close();

            }
            catch (Exception erreur2)
            {
                MessageBox.Show("Connexion échouée :\n\n" + erreur2);
            }

        }
    }
}
