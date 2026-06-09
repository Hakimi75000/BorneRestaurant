// Importation des classes du projet
using BorneRestaurant.models;
using BorneRestaurant.Services;

// ObservableCollection permet de mettre à jour automatiquement l'interface
// lorsqu'on ajoute ou supprime des éléments dans une liste.
using System.Collections.ObjectModel;

using System.Windows;
using System.Windows.Controls;

namespace BorneRestaurant.views
{
    // Fenêtre principale du menu du restaurant
    public partial class MenuWindow : Window
    {
        // Liste des catégories affichées dans l'interface
        private ObservableCollection<Categorie> categories =
        new ObservableCollection<Categorie>();


    // Services permettant d'accéder aux données de la base MySQL
    private readonly CategorieService categorieService;
        private readonly ProduitService produitService;

        // Constructeur de la fenêtre
        public MenuWindow(string mode)
        {
            InitializeComponent();

            // Création des services
            categorieService = new CategorieService();
            produitService = new ProduitService();

            // Affichage du mode choisi (Sur place / À emporter)
            TxtMode.Text = mode;

            // Chargement des catégories depuis la base de données
            ChargerCategories();

            // Liaison de la liste des catégories avec le contrôle ListBox
            ListCategories.ItemsSource = categories;

            // Sélection automatique de la première catégorie
            if (categories.Count > 0)
                ListCategories.SelectedIndex = 0;
        }

        // Charge toutes les catégories depuis la base
        private void ChargerCategories()
        {
            // On vide la liste actuelle
            categories.Clear();

            // On ajoute les catégories récupérées par le service
            foreach (var cat in categorieService.GetCategories())
            {
                categories.Add(cat);
            }
        }

        // Charge les produits de la catégorie sélectionnée
        private void ChargerProduits(int categorieId)
        {
            GridProduits.ItemsSource =
                produitService.GetProduits(categorieId);
        }

        // Événement déclenché lorsqu'on sélectionne une catégorie
        private void ListCategories_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e)
        {
            var cat = ListCategories.SelectedItem as Categorie;

            // Sécurité : si aucune catégorie n'est sélectionnée
            if (cat == null)
                return;

            // Chargement des produits associés à la catégorie
            ChargerProduits(cat.id);
        }

        // Bouton Ajouter un produit
        private void BtnAjoutProduit_Click(
            object sender,
            RoutedEventArgs e)
        {
            var cat = ListCategories.SelectedItem as Categorie;

            // Vérifie qu'une catégorie est sélectionnée
            if (cat == null)
            {
                MessageBox.Show(
                    "Choisis d'abord une catégorie.");
                return;
            }

            // Ouverture de la fenêtre d'ajout
            var ajoutProduit =
                new AjoutProduit(cat.id);

            ajoutProduit.ShowDialog();

            // Rafraîchissement de la liste après ajout
            ChargerProduits(cat.id);
        }

        // Bouton Ajouter une catégorie
        private void BtnAjoutCategorie_Click(
            object sender,
            RoutedEventArgs e)
        {
            var ajoutCategorie =
                new AjoutCategorie();

            ajoutCategorie.ShowDialog();

            // Rechargement des catégories
            ChargerCategories();

            // Sélection automatique de la première catégorie
            if (categories.Count > 0)
                ListCategories.SelectedIndex = 0;
        }

        // Retour vers la fenêtre précédente
        private void BtnRetour_Click(
            object sender,
            RoutedEventArgs e)
        {
            ChoixWindow choix = new ChoixWindow();

            choix.Show();

            Close();
        }

        // Bouton Supprimer un produit
        private void BtnSupprimerProduit_Click(
            object sender,
            RoutedEventArgs e)
        {
            var produit =
                GridProduits.SelectedItem as Produit;

            // Vérifie qu'un produit est sélectionné
            if (produit == null)
            {
                MessageBox.Show(
                    "Sélectionne un produit à supprimer.");
                return;
            }

            // Demande de confirmation
            var confirm = MessageBox.Show(
                $"Supprimer '{produit.nom}' ?",
                "Confirmation",
                MessageBoxButton.YesNo);

            if (confirm != MessageBoxResult.Yes)
                return;

            // Suppression du produit via le service
            produitService.SupprimerProduit(produit.id);

            // Rechargement de la liste
            var cat =
                ListCategories.SelectedItem as Categorie;

            if (cat != null)
                ChargerProduits(cat.id);
        }

        // Bouton Supprimer une catégorie
        private void BtnSupprimerCategorie_Click(
            object sender,
            RoutedEventArgs e)
        {
            var cat =
                ListCategories.SelectedItem as Categorie;

            // Vérifie qu'une catégorie est sélectionnée
            if (cat == null)
            {
                MessageBox.Show(
                    "Sélectionne une catégorie.");
                return;
            }

            // Demande de confirmation
            var confirm = MessageBox.Show(
                $"Supprimer la catégorie '{cat.nom}' ?",
                "Confirmation",
                MessageBoxButton.YesNo);

            if (confirm != MessageBoxResult.Yes)
                return;

            try
            {
                // Suppression via le service
                categorieService.SupprimerCategorie(cat.id);
            }
            catch
            {
                // Gestion du cas où la catégorie contient encore des produits
                MessageBox.Show(
                    "Impossible de supprimer cette catégorie car elle contient des produits.");
                return;
            }

            // Rafraîchissement de l'affichage
            ChargerCategories();

            GridProduits.ItemsSource = null;
        }

        // Bouton Modifier une catégorie
        private void BtnModifierCategorie_Click(
            object sender,
            RoutedEventArgs e)
        {
            var cat =
                ListCategories.SelectedItem as Categorie;

            if (cat == null)
            {
                MessageBox.Show(
                    "Sélectionne une catégorie.");
                return;
            }

            // Ouverture de la fenêtre de modification
            var w = new ModifierCategorie(cat);

            bool? ok = w.ShowDialog();

            // Si modification validée
            if (ok == true)
            {
                ChargerCategories();
            }
        }

        // Bouton Modifier un produit
        private void BtnModifierProduit_Click(
            object sender,
            RoutedEventArgs e)
        {
            var produit =
                GridProduits.SelectedItem as Produit;

            if (produit == null)
            {
                MessageBox.Show(
                    "Sélectionne un produit.");
                return;
            }

            // Ouverture de la fenêtre de modification
            var w = new ModifierProduit(produit);

            bool? ok = w.ShowDialog();

            // Si modification validée
            if (ok == true)
            {
                var cat =
                    ListCategories.SelectedItem as Categorie;

                if (cat != null)
                    ChargerProduits(cat.id);
            }
        }
    }


}
