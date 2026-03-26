using System.Windows;
using BorneRestaurant.views;

namespace BorneRestaurant
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnEntrer_Click(object sender, RoutedEventArgs e)
        {
            ChoixWindow choix = new ChoixWindow();
            choix.Show();
            this.Close();
        }
    }
}
