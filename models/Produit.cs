// Classe représentant un produit du restaurant
// (Burger, Coca-Cola, Dessert, etc.)

namespace BorneRestaurant.models
{
    public class Produit
    {
        // Identifiant unique du produit dans la base de données
        public int id { get; set; }


    // Nom du produit
    public string nom { get; set; }

        // Prix du produit
        // Le type decimal est recommandé pour les montants financiers
        public decimal prix { get; set; }

        // Description du produit
        public string description { get; set; }

        // Clé étrangère permettant de savoir
        // à quelle catégorie appartient le produit
        public int categorie_id { get; set; }

        // Quantité disponible en stock
        // Prévue pour une évolution future du projet
        public int quantite { get; set; }

        // Statut du produit
        // Exemple : Disponible / Indisponible
        // Prévu pour une évolution future du projet
        public string statut { get; set; }
    }


}
