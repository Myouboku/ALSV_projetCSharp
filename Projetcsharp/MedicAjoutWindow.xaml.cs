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

        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Laboratoire
            List<Combobox> listLab = new List<Combobox>();
            var ProcLab = "PS_Recuperation_Laboratoires";
           var cmdLab = new SqlCommand(ProcLab, Conn);
            SqlDataReader drLab = cmdLab.ExecuteReader();
            while (drLab.Read())
            {
                listLab.Add(new Combobox { Id = (Int16)drLab["LAB_ID"],
                Nom = (string)drLab["LAB_NOM"]
                });

            } 
            comboBoxLabo.ItemsSource = listLab;
            comboBoxLabo.DisplayMemberPath = "Nom";

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
            comboBoxAsp.ItemsSource = listAsp;
            comboBoxAsp.DisplayMemberPath = "Nom";


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
            comboBoxCont.ItemsSource = listCont;
            comboBoxCont.DisplayMemberPath = "Nom";

            //Effet Secondaire
            List<Combobox> listEffSecon = new List<Combobox>();
            var procEffSecon = "PS_Recuperation_EffetsSecondaires";
            var cmdEffSecond = new SqlCommand(procEffSecon, Conn);
            SqlDataReader drEffSecond = cmdEffSecond.ExecuteReader();
            while(drEffSecond.Read())
            {
                listEffSecon.Add(new Combobox
                {
                    Id = (Int16)drEffSecond["EFF_ID"],
                    Nom = (string)drEffSecond["EFF_LIBELLE"]
                });
            }
            comboBoxEff.ItemsSource = listEffSecon;
            comboBoxEff.DisplayMemberPath = "Nom";

        }
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
                    command.Parameters.AddWithValue("LAB_ID", ((Combobox)comboBoxLabo.SelectedItem).Id);
                    command.Parameters.AddWithValue("ASP_ID", ((Combobox)comboBoxAsp.SelectedItem).Id);
                    command.Parameters.AddWithValue("UNI_ID", ((Combobox)comboboxUni.SelectedItem).Id);
                    command.Parameters.AddWithValue("EFF_ID", ((Combobox)comboBoxEff.SelectedItem).Id);
                    command.Parameters.AddWithValue("CON_ID", ((Combobox)comboBoxCont.SelectedItem).Id);

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
