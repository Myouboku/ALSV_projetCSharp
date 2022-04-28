using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Logique d'interaction pour ModifPraticien.xaml
    /// </summary>
    public partial class ModifPraticien : Window
    {
        public SqlConnection Conn { get; set; }

        public ModifPraticien(System.Data.SqlClient.SqlConnection conn)
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

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            txtboxLogin.Text = txtboxNom.Text + "." + txtboxPrenom.Text;
        }

        private void txtboxPrenom_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtboxLogin.Text = txtboxNom.Text + "." + txtboxPrenom.Text;
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
