using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BorneRestaurant.models;
using BorneRestaurant.views;


namespace BorneRestaurant.views
{
    public partial class ChoixWindow : Window
    {
        public ChoixWindow()
        {
            InitializeComponent();
        }

        private void BtnEmporter_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow menuWindow = new MenuWindow("À emporter");
            menuWindow.Show();
            this.Close();
        }

        private void BtnSurplace_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow menuWindow = new MenuWindow("Sur place");
            menuWindow.Show();
            this.Close();
        }

        private void BtnRetour_Click(object sender, RoutedEventArgs e)
        {
            MainWindow choix = new MainWindow();
            choix.Show();
            this.Close();
        }


    }
}
