using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Projetcsharp
{
    /// <summary>
    /// Logique d'interaction pour AjoutPraticien.xaml
    /// </summary>
    public partial class AjoutPraticien : Window
    {
        public SqlConnection Conn { get; set; }

        public AjoutPraticien(System.Data.SqlClient.SqlConnection conn)
        {
            InitializeComponent();
            Conn = conn;
            Fillcombo();
        }

        public void Fillcombo()
        {
            var proc = "PS_ListeDisipline";
            var command = new SqlCommand(proc, Conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            var data = command.ExecuteReader();
            cbBoxDiscipline.ItemsSource = data;
            cbBoxDiscipline.DisplayMemberPath = "DIS_LIBELLE";
        }

        private void txtBoxCP_TextChanged(object sender, TextChangedEventArgs e)
        {
            var Patern = new Regex("^[0-9]*$");
            if (!Patern.Match(((TextBox)sender).Text).Success)
            {
                txtBoxCP.Text = txtBoxCP.Text.Substring(0, txtBoxCP.Text.Length - 1);
            }
        }

        private void btnAjoutPraticien_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using SqlCommand command = new("PS_I_Praticien", Conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("PRA_NOM", txtboxNom.Text);
                command.Parameters.AddWithValue("PRA_PRENOM", txtboxPrenom.Text);
                command.Parameters.AddWithValue("PRA_ADRESSE", txtboxAdresse.Text);
                command.Parameters.AddWithValue("PRA_CP", Convert.ToInt32(txtBoxCP.Text));
                command.Parameters.AddWithValue("PRA_VILLE", txtBoxVille.Text);
                command.Parameters.AddWithValue("DIS_ID", ((Combobox)cbBoxDiscipline.SelectedItem).Id);

                var datas = command.ExecuteReader();
                datas.Read();

                PraticienWindow window = new(Conn);
                window.ShowDialog();
                Close();
            }
            catch (Exception erreur2)
            {
                MessageBox.Show("Connexion échouée :\n\n" + erreur2);
            }
        }

        private void WindowsLoaded(object sender, RoutedEventArgs e)
        {
            //Discipline
            List<Combobox> listDis = new List<Combobox>();
            const ProcDis = "PS_ListeDisipline";
            var cmdDis = new SqlCommand(ProcDis, Conn);
            SqlDataReader drDis = cmdDis.ExecuteReader();
            while (drDis.Read())
            {
                listDis.Add(new Combobox
                {
                    Id = (Int16)drDis["DIS_ID"],
                    Nom = (string)drDis["DIS_LIBELLE"]
                });

            }
            cbBoxDiscipline.ItemsSource = listDis;
            cbBoxDiscipline.DisplayMemberPath = "Nom";
        }
    }
}
