﻿using System.Data;
using System.Data.SqlClient;
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
    }
}
