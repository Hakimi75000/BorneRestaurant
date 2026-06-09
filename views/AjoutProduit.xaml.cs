// Permet d'utiliser les fenêtres WPF
using System.Windows;

// Permet d'utiliser le ViewModel associé à cette fenêtre
using BorneRestaurant.ViewModels;

namespace BorneRestaurant.views
{
    // Fenêtre permettant d'ajouter un produit
    public partial class AjoutProduit : Window
    {
        // Constructeur de la fenêtre
        public AjoutProduit(int categorieId)
        {
            // Initialise tous les composants définis dans le XAML
            InitializeComponent();


        // Association de la vue avec son ViewModel
        // On transmet l'identifiant de la catégorie afin que
        // le nouveau produit soit rattaché à la bonne catégorie.
        DataContext =
            new AjoutProduitViewModel(categorieId);
        }
    }


}
