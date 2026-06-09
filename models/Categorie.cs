// Permet d'utiliser ObservableCollection
using System.Collections.ObjectModel;

namespace BorneRestaurant.models
{
    // Classe représentant une catégorie de produits
    // (Boissons, Burgers, Desserts...)
    public class Categorie
    {
        // Identifiant unique de la catégorie dans la base de données
        public int id { get; set; }


    // Nom de la catégorie
    public string nom { get; set; }

        // Description de la catégorie
        public string description { get; set; }

        // Liste des produits appartenant à cette catégorie
        // ObservableCollection permet de mettre à jour
        // automatiquement l'interface WPF lorsqu'on ajoute
        // ou supprime un produit.
        public ObservableCollection<Produit> produits { get; set; }
            = new ObservableCollection<Produit>();
    }


}
