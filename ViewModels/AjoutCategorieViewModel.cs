// Permet d'utiliser les MessageBox
using System.Windows;

// Permet d'utiliser les commandes WPF
using System.Windows.Input;

// Permet d'utiliser RelayCommand
using BorneRestaurant.Commands;

// Permet d'utiliser le service de gestion des catégories
using BorneRestaurant.Services;

namespace BorneRestaurant.ViewModels
{
    // ViewModel associé à la fenêtre AjoutCategorie.
    // Il contient les données et les actions utilisées par l'interface.
    public class AjoutCategorieViewModel : BaseViewModel
    {
        // Service permettant de communiquer avec la base de données
        private readonly CategorieService categorieService;


    // Variables privées stockant les valeurs saisies
    private string nom;
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

        // Propriété liée à la TextBox de description
        public string Description
        {
            get => description;
            set
            {
                description = value;

                // Met à jour automatiquement l'affichage
                OnPropertyChanged();
            }
        }

        // Commande associée au bouton Ajouter
        public ICommand AjouterCategorieCommand { get; }

        // Constructeur du ViewModel
        public AjoutCategorieViewModel()
        {
            // Création du service
            categorieService = new CategorieService();

            // Association du bouton à la méthode AjouterCategorie
            AjouterCategorieCommand =
                new RelayCommand(AjouterCategorie);
        }

        // Méthode exécutée lorsque l'utilisateur
        // clique sur le bouton Ajouter
        private void AjouterCategorie()
        {
            // Vérifie que le nom est renseigné
            if (string.IsNullOrWhiteSpace(Nom))
            {
                MessageBox.Show("Le nom est obligatoire.");
                return;
            }

            // Ajout de la catégorie dans la base
            categorieService.AjouterCategorie(
                Nom.Trim(),
                Description?.Trim());

            MessageBox.Show("Catégorie ajoutée !");

            // Réinitialisation des champs après l'ajout
            Nom = "";
            Description = "";

            // Mise à jour de l'interface
            OnPropertyChanged(nameof(Nom));
            OnPropertyChanged(nameof(Description));
        }
    }


}
