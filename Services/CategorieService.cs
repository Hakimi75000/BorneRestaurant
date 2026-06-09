// Permet d'utiliser la classe de connexion à la base de données
using BorneRestaurant.data;

// Permet d'utiliser le modèle Categorie
using BorneRestaurant.models;

// Permet d'utiliser les classes MySQL
using MySql.Data.MySqlClient;

// Permet d'utiliser ObservableCollection
using System.Collections.ObjectModel;

namespace BorneRestaurant.Services
{
    // Service chargé de gérer toutes les opérations
    // liées aux catégories dans la base de données.
    public class CategorieService
    {
        // Récupère toutes les catégories de la base
        public ObservableCollection<Categorie> GetCategories()
        {
            // Création d'une liste vide
            var categories = new ObservableCollection<Categorie>();


        // Création de la connexion à la base
        using (var conn = Db.GetConnection())
            {
                // Ouverture de la connexion
                conn.Open();

                // Requête SQL permettant de récupérer
                // toutes les catégories triées par nom
                string sql =
                    "SELECT id, nom, description FROM categories ORDER BY nom;";

                // Préparation de la commande SQL
                using (var cmd = new MySqlCommand(sql, conn))

                // Exécution de la requête
                using (var reader = cmd.ExecuteReader())
                {
                    // Lecture des résultats ligne par ligne
                    while (reader.Read())
                    {
                        // Création d'un objet Categorie
                        // à partir des données récupérées
                        categories.Add(new Categorie
                        {
                            id = reader.GetInt32("id"),
                            nom = reader.GetString("nom"),

                            // Vérifie si la description est NULL
                            description =
                                reader.IsDBNull(reader.GetOrdinal("description"))
                                ? ""
                                : reader.GetString("description")
                        });
                    }
                }
            }

            // Retourne la liste des catégories
            return categories;
        }

        // Ajoute une nouvelle catégorie dans la base
        public void AjouterCategorie(string nom, string description)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();

                // Requête SQL d'insertion
                string sql =
                @"INSERT INTO categories
              (nom, description)
              VALUES
              (@nom, @description);";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    // Paramètres de la requête
                    cmd.Parameters.AddWithValue("@nom", nom);
                    cmd.Parameters.AddWithValue("@description", description);

                    // Exécution de la requête
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Modifie une catégorie existante
        public void ModifierCategorie(Categorie categorie)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();

                // Requête SQL de mise à jour
                string sql =
                @"UPDATE categories
              SET nom = @nom,
                  description = @description
              WHERE id = @id;";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    // Envoi des nouvelles valeurs
                    cmd.Parameters.AddWithValue("@id", categorie.id);
                    cmd.Parameters.AddWithValue("@nom", categorie.nom);
                    cmd.Parameters.AddWithValue("@description", categorie.description);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Supprime une catégorie grâce à son identifiant
        public void SupprimerCategorie(int id)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();

                // Requête SQL de suppression
                string sql =
                    "DELETE FROM categories WHERE id = @id;";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }


}
