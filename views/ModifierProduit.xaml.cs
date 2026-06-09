// Permet d'utiliser le modèle Produit
using BorneRestaurant.models;

// Permet d'utiliser le service qui communique avec la base de données
using BorneRestaurant.Services;

// Permet de gérer les conversions de nombres
// (notamment pour le prix)
using System.Globalization;

// Permet d'utiliser les composants WPF
using System.Windows;

namespace BorneRestaurant.views
{
    // Fenêtre permettant de modifier un produit existant
    public partial class ModifierProduit : Window
    {
        // Service chargé de communiquer avec la base de données
        private readonly ProduitService produitService;


    // Identifiant du produit à modifier
    private readonly int idProduit;

        // Constructeur de la fenêtre
        // Reçoit le produit sélectionné dans le menu
        public ModifierProduit(Produit p)
        {
            InitializeComponent();

            // Création du service
            produitService = new ProduitService();

            // Sauvegarde de l'identifiant du produit
            idProduit = p.id;

            // Pré-remplissage des champs avec les données existantes
            TxtNom.Text = p.nom;

            // Conversion du prix en texte pour l'affichage
            TxtPrix.Text = p.prix.ToString(CultureInfo.InvariantCulture);

            TxtDescription.Text = p.description;
        }

        // Événement exécuté lorsque l'utilisateur clique sur Enregistrer
        private void BtnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            // Vérifie que le nom du produit est renseigné
            if (string.IsNullOrWhiteSpace(TxtNom.Text))
            {
                MessageBox.Show("Le nom est obligatoire.");
                return;
            }

            // Récupération du prix saisi par l'utilisateur
            // et remplacement de la virgule par un point
            string prixSaisi =
                TxtPrix.Text.Trim().Replace(',', '.');

            // Vérifie que le prix est bien un nombre valide
            if (!decimal.TryParse(
                prixSaisi,
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out decimal prix))
            {
                MessageBox.Show("Prix invalide (ex : 9,90).");
                return;
            }

            // Création d'un objet Produit contenant
            // les nouvelles informations saisies
            Produit produit = new Produit
            {
                id = idProduit,
                nom = TxtNom.Text.Trim(),
                description = TxtDescription.Text.Trim(),
                prix = prix
            };

            // Mise à jour du produit dans la base de données
            // via le service
            produitService.ModifierProduit(produit);

            // Indique que la modification s'est bien déroulée
            DialogResult = true;

            // Fermeture de la fenêtre
            Close();
        }
    }


}
