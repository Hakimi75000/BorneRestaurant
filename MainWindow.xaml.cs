// Permet d'utiliser les composants WPF
using System.Windows;

// Permet d'accéder aux autres fenêtres du projet
using BorneRestaurant.views;

namespace BorneRestaurant
{
    // Fenêtre d'accueil de l'application.
    // C'est la première fenêtre affichée au démarrage.
    public partial class MainWindow : Window
    {
        // Constructeur de la fenêtre
        public MainWindow()
        {
            // Charge les composants définis dans MainWindow.xaml
            InitializeComponent();
        }


    // Événement exécuté lorsque l'utilisateur
    // clique sur le bouton "Entrer"
    private void BtnEntrer_Click(object sender, RoutedEventArgs e)
        {
            // Création de la fenêtre permettant de choisir
            // le mode de commande (sur place ou à emporter)
            ChoixWindow choix = new ChoixWindow();

            // Affichage de la nouvelle fenêtre
            choix.Show();

            // Fermeture de la fenêtre actuelle
            this.Close();
        }
    }


}
