// Permet d'utiliser les composants WPF (Window, boutons, etc.)
using System.Windows;

// Permet d'utiliser le ViewModel de la fenêtre
using BorneRestaurant.ViewModels;

namespace BorneRestaurant.views
{
    // Fenêtre permettant d'ajouter une nouvelle catégorie
    public partial class AjoutCategorie : Window
    {
        // Constructeur de la fenêtre
        public AjoutCategorie()
        {
            // Initialise tous les composants définis dans le fichier XAML
            InitializeComponent();


        // Association de la vue avec son ViewModel
        // Le ViewModel contient les données et les commandes
        // utilisées par l'interface graphique.
        DataContext =
            new AjoutCategorieViewModel();
        }
    }


}
