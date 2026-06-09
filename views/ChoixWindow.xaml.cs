// Permet d'utiliser les composants WPF (fenêtres, boutons...)
using System.Windows;

namespace BorneRestaurant.views
{
    // Fenêtre permettant au client de choisir
    // entre une commande sur place ou à emporter.
    public partial class ChoixWindow : Window
    {
        // Constructeur de la fenêtre
        public ChoixWindow()
        {
            // Charge les composants définis dans le XAML
            InitializeComponent();
        }


    // Bouton "À emporter"
    private void BtnEmporter_Click(object sender, RoutedEventArgs e)
        {
            // Création de la fenêtre Menu
            // en indiquant le mode "À emporter"
            MenuWindow menuWindow = new MenuWindow("À emporter");

            // Affichage de la fenêtre
            menuWindow.Show();

            // Fermeture de la fenêtre actuelle
            this.Close();
        }

        // Bouton "Sur place"
        private void BtnSurplace_Click(object sender, RoutedEventArgs e)
        {
            // Création de la fenêtre Menu
            // en indiquant le mode "Sur place"
            MenuWindow menuWindow = new MenuWindow("Sur place");

            // Affichage de la fenêtre
            menuWindow.Show();

            // Fermeture de la fenêtre actuelle
            this.Close();
        }

        // Bouton Retour
        private void BtnRetour_Click(object sender, RoutedEventArgs e)
        {
            // Retour vers l'écran d'accueil
            MainWindow choix = new MainWindow();

            // Affichage de la fenêtre d'accueil
            choix.Show();

            // Fermeture de la fenêtre actuelle
            this.Close();
        }
    }


}
