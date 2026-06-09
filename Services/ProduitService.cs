// Permet d'utiliser la classe de connexion à la base de données
using BorneRestaurant.data;

// Permet d'utiliser le modèle Produit
using BorneRestaurant.models;

// Permet d'utiliser les classes MySQL
using MySql.Data.MySqlClient;

// Permet d'utiliser ObservableCollection
using System.Collections.ObjectModel;

namespace BorneRestaurant.Services
{
    // Service chargé de gérer toutes les opérations
    // liées aux produits dans la base de données.
    public class ProduitService
    {
        // Récupère tous les produits d'une catégorie
        public ObservableCollection<Produit> GetProduits(int categorieId)
        {
            // Création d'une liste vide
            var produits = new ObservableCollection<Produit>();


        // Ouverture d'une connexion à la base
        using (var conn = Db.GetConnection())
            {
                conn.Open();

                // Requête SQL permettant de récupérer
                // tous les produits de la catégorie sélectionnée
                string sql =
                @"SELECT id,
                     nom,
                     description,
                     prix,
                     categorie_id
              FROM produits
              WHERE categorie_id = @id
              ORDER BY nom;";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    // Paramètre permettant de filtrer
                    // les produits selon la catégorie choisie
                    cmd.Parameters.AddWithValue("@id", categorieId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        // Lecture des résultats ligne par ligne
                        while (reader.Read())
                        {
                            produits.Add(new Produit
                            {
                                id = reader.GetInt32("id"),
                                nom = reader.GetString("nom"),

                                // Vérifie si la description est NULL
                                description =
                                    reader.IsDBNull(reader.GetOrdinal("description"))
                                    ? ""
                                    : reader.GetString("description"),

                                prix = reader.GetDecimal("prix"),

                                categorie_id =
                                    reader.GetInt32("categorie_id")
                            });
                        }
                    }
                }
            }

            // Retourne la liste des produits
            return produits;
        }

        // Supprime un produit grâce à son identifiant
        public void SupprimerProduit(int id)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();

                // Requête SQL de suppression
                string sql =
                    "DELETE FROM produits WHERE id = @id;";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Modifie un produit existant
        public void ModifierProduit(Produit produit)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();

                // Requête SQL de mise à jour
                string sql =
                @"UPDATE produits
              SET nom = @nom,
                  description = @description,
                  prix = @prix
              WHERE id = @id;";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    // Envoi des nouvelles valeurs
                    cmd.Parameters.AddWithValue("@id", produit.id);
                    cmd.Parameters.AddWithValue("@nom", produit.nom);
                    cmd.Parameters.AddWithValue("@description", produit.description);
                    cmd.Parameters.AddWithValue("@prix", produit.prix);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Ajoute un nouveau produit
        public void AjouterProduit(Produit produit)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();

                // Requête SQL d'insertion
                string sql =
                @"INSERT INTO produits
              (nom, description, prix, categorie_id)
              VALUES
              (@nom, @description, @prix, @categorie_id);";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    // Paramètres de la requête
                    cmd.Parameters.AddWithValue("@nom", produit.nom);
                    cmd.Parameters.AddWithValue("@description", produit.description);
                    cmd.Parameters.AddWithValue("@prix", produit.prix);

                    // Clé étrangère reliant le produit
                    // à sa catégorie
                    cmd.Parameters.AddWithValue(
                        "@categorie_id",
                        produit.categorie_id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }


}
