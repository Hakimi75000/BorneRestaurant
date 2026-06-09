// Permet de gérer les conversions de nombres
// (par exemple convertir un texte en prix)
using System.Globalization;

// Permet d'utiliser les MessageBox
using System.Windows;

// Permet d'utiliser ICommand
using System.Windows.Input;

// Permet d'utiliser RelayCommand
using BorneRestaurant.Commands;

// Permet d'utiliser le modèle Produit
using BorneRestaurant.models;

// Permet d'utiliser le service de gestion des produits
using BorneRestaurant.Services;

namespace BorneRestaurant.ViewModels
{
    // ViewModel associé à la fenêtre AjoutProduit.
    // Il contient les données et les actions utilisées par l'interface.
    public class AjoutProduitViewModel : BaseViewModel
    {
        // Service permettant d'accéder à la base de données
        private readonly ProduitService service;


    // Identifiant de la catégorie sélectionnée
    // Le produit ajouté sera lié à cette catégorie
    private readonly int categorieId;

        // Variables privées contenant les données saisies
        private string nom;
        private string prix;
        private string description;

        // Propriété liée à la TextBox du nom
        public string Nom
        {
            get => nom;
            set
            {
                nom = value;

                // Informe l'interface qu'une valeur a changé
                OnPropertyChanged();
            }
        }

        // Propriété liée à la TextBox du prix
        public string Prix
        {
            get => prix;
            set
            {
                prix = value;

                // Mise à jour automatique de l'interface
                OnPropertyChanged();
            }
        }

        // Propriété liée à la TextBox de description
        public string Description
        {
            get => description;
            set
            {
                description = value;

                // Mise à jour automatique de l'interface
                OnPropertyChanged();
            }
        }

        // Commande associée au bouton Ajouter
        public ICommand AjouterProduitCommand { get; }

        // Constructeur du ViewModel
        public AjoutProduitViewModel(int categorieId)
        {
            // Sauvegarde de la catégorie sélectionnée
            this.categorieId = categorieId;

            // Création du service
            service = new ProduitService();

            // Association du bouton à la méthode AjouterProduit
            AjouterProduitCommand =
                new RelayCommand(AjouterProduit);
        }

        // Méthode exécutée lorsque l'utilisateur clique
        // sur le bouton Ajouter
        private void AjouterProduit()
        {
            // Vérifie que le nom est renseigné
            if (string.IsNullOrWhiteSpace(Nom))
            {
                MessageBox.Show("Le nom est obligatoire.");
                return;
            }

            // Nettoyage du prix saisi
            // Remplace la virgule par un point
            string prixFormate =
                Prix?.Trim().Replace(',', '.');

            // Vérifie que le prix est valide
            if (!decimal.TryParse(
                prixFormate,
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out decimal prixDecimal))
            {
                MessageBox.Show("Prix invalide.");
                return;
            }

            // Création de l'objet Produit
            Produit produit = new Produit
            {
                nom = Nom,
                prix = prixDecimal,
                description = Description,

                // Association du produit à la catégorie sélectionnée
                categorie_id = categorieId
            };

            // Enregistrement dans la base de données
            service.AjouterProduit(produit);

            MessageBox.Show("Produit ajouté !");

            // Réinitialisation des champs après l'ajout
            Nom = "";
            Prix = "";
            Description = "";
        }
    }


}
