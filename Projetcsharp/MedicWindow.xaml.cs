using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Projetcsharp
{
    /// <summary>
    /// Logique d'interaction pour MedicWindow.xaml
    /// </summary>
    public partial class MedicWindow : Window
    {
        public MedicWindow()
        {
            InitializeComponent();
        }

        private void btnDeconnect_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnModification_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnAjout_Click(object sender, RoutedEventArgs e)
        { 
            
        }

        private void btnSupprime_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
