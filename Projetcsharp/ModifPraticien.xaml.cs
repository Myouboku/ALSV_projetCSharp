using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Projetcsharp
{
    /// <summary>
    /// Logique d'interaction pour ModifPraticien.xaml
    /// </summary>
    public partial class ModifPraticien : Window
    {
        public SqlConnection Conn { get; set; }
        public Praticien praticien { get; set; }

        public ModifPraticien(System.Data.SqlClient.SqlConnection conn, Praticien donnees)
        {
            InitializeComponent();
            Conn = conn;
            Fillcombo();

            praticien = donnees;
            txtboxNom.Text = praticien.Nom;
            txtboxPrenom.Text = praticien.Prenom;
            txtboxAdresse.Text = praticien.Adresse;
            txtBoxCP.Text = praticien.CP.ToString();
            txtBoxVille.Text = praticien.Ville;
            cbBoxDiscipline.Text = praticien.Discipline;
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

        private void btnModifierPraticien_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using SqlCommand command = new("PS_E_Praticien", Conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("PRA_ID", praticien.ID);
                command.Parameters.AddWithValue("PRA_NOM", txtboxNom.Text);
                command.Parameters.AddWithValue("PRA_PRENOM", txtboxPrenom.Text);
                command.Parameters.AddWithValue("PRA_CP", Convert.ToInt32(txtBoxCP.Text));
                command.Parameters.AddWithValue("PRA_ADRESSE", txtboxAdresse.Text);
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

        private void WindowsLoading(object sender, RoutedEventArgs e)
        {
            using SqlCommand comdPratic = new("PS_Recuperation_PraticienId", Conn);
            comdPratic.CommandType = CommandType.StoredProcedure;
            comdPratic.Parameters.AddWithValue("PRA_ID", praticien.ID);
            var datas = comdPratic.ExecuteReader();
            datas.Read();
            txtboxNom.Text = datas["PRA_NOM"].ToString();
            txtboxPrenom.Text = datas["PRA_PRENOM"].ToString();
            txtboxAdresse.Text = datas["PRA_ADRESSE"].ToString();
            txtBoxCP.Text = datas["PRA_CP"].ToString();
            txtBoxVille.Text = datas["PRA_VILLE"].ToString();

            //Discipline
            List<Combobox> listDis = new List<Combobox>();
            string ProcDis = "PS_ListeDisipline";
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
            cbBoxDiscipline.SelectedItem = listDis.First(x => x.Id == (Int16)datas["DIS_ID"]);

        }
    }
}
