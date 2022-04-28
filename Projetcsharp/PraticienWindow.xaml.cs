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
    /// Logique d'interaction pour PraticienWindow.xaml
    /// </summary>
    public partial class PraticienWindow : Window
    {
        public SqlConnection Conn { get; set; }

        public void DGReload()
        {
            DGpraticien.Items.Refresh();

            var proc = "PS_Affichage_Praticien";
            var command = new SqlCommand(proc, Conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            var data = command.ExecuteReader();

            List<Praticien> list = new List<Praticien>();
            while (data.Read())
            {
                list.Add(new Praticien { 
                    ID=(Int16)data["Id"],
                    Nom= (string)data["Nom"], 
                    Prenom= (string)data["Prénom"], 
                    CP= (int)data["Code Postal"], 
                    Discipline=(string)data["Discipline"],
                    Adresse= (string)data["Adresse"],
                    Ville= (string)data["Ville"]
                });
            }
            
            DGpraticien.ItemsSource = list;
            DGpraticien.Columns[0].Visibility = Visibility.Hidden;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DGReload(); // charge le tableeau au chargement
        }

        public PraticienWindow(SqlConnection conn)
        {
            InitializeComponent();
            Conn = conn;
        }

        private void btnSupprimePraticien_Click(object sender, RoutedEventArgs e)
        {
            if (DGpraticien.SelectedItem == null)
                MessageBox.Show("La ligne selectionnée est nulle", "Erreur");
            else
            {
                MessageBoxResult dialogResult = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette donnée ?", "Attention", MessageBoxButton.YesNo);

                if (dialogResult == MessageBoxResult.Yes)
                {
                    var proc = "PS_D_Praticien";
                    var command = new SqlCommand(proc, Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    SqlParameter param1 = new("@PRA_ID", SqlDbType.Int)
                    {
                        Value = ((Praticien)DGpraticien.SelectedItem).ID
                    };
                    command.Parameters.Add(param1);
                    command.ExecuteReader();
                }
                DGReload(); // recharge la tableau au clic du bouton
            }
        }

        private void btnAjoutPraticien_Click(object sender, RoutedEventArgs e)
        {
            AjoutPraticien window = new(Conn);
            window.Show();
        }

        private void btnModificationPraticien_Click(object sender, RoutedEventArgs e)
        {
            if (DGpraticien.SelectedItem == null)
            {
                MessageBox.Show("La ligne selectionnée est nulle", "Erreur");
                
            }
            else
            {
                ModifPraticien window = new(Conn, (Praticien)DGpraticien.SelectedItem);
                window.Show();
            }
        }
    }
}
