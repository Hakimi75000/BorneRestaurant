// Permet d'utiliser le modèle Categorie
using BorneRestaurant.models;

// Permet d'utiliser le service qui communique avec la base de données
using BorneRestaurant.Services;

// Permet d'utiliser les composants WPF
using System.Windows;

namespace BorneRestaurant.views
{
    // Fenêtre permettant de modifier une catégorie existante
    public partial class ModifierCategorie : Window
    {
        // Service utilisé pour accéder à la base de données
        private readonly CategorieService categorieService;


    // Stocke l'identifiant de la catégorie à modifier
    private readonly int idCategorie;

        // Constructeur de la fenêtre
        // Reçoit en paramètre la catégorie sélectionnée
        public ModifierCategorie(Categorie cat)
        {
            InitializeComponent();

            // Création du service
            categorieService = new CategorieService();

            // Sauvegarde de l'identifiant
            idCategorie = cat.id;

            // Pré-remplissage des champs avec les données actuelles
            TxtNom.Text = cat.nom;
            TxtDescription.Text = cat.description;
        }

        // Événement déclenché lorsqu'on clique sur le bouton Enregistrer
        private void BtnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            // Vérifie que le nom n'est pas vide
            if (string.IsNullOrWhiteSpace(TxtNom.Text))
            {
                MessageBox.Show("Le nom est obligatoire.");
                return;
            }

            // Création d'un objet Categorie contenant
            // les nouvelles valeurs saisies par l'utilisateur
            Categorie categorie = new Categorie
            {
                id = idCategorie,
                nom = TxtNom.Text.Trim(),
                description = TxtDescription.Text.Trim()
            };

            // Appel du service pour mettre à jour
            // la catégorie dans la base de données
            categorieService.ModifierCategorie(categorie);

            // Indique à la fenêtre précédente
            // que la modification s'est bien déroulée
            DialogResult = true;

            // Fermeture de la fenêtre
            Close();
        }
    }


}
