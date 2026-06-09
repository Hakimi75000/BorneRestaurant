// Permet d'utiliser le connecteur MySQL
using MySql.Data.MySqlClient;

namespace BorneRestaurant.data
{
    // Classe statique utilisée pour gérer
    // la connexion à la base de données.
    public static class Db
    {
        // Chaîne de connexion à la base MySQL.
        // Elle contient :
        // - le serveur
        // - le port
        // - le nom de la base
        // - l'utilisateur
        // - le mot de passe
        public static string ConnStr =
        "Server=localhost;Port=3306;Database=borne_restaurant;Uid=root;Pwd=;";


    // Méthode permettant de créer
    // une nouvelle connexion MySQL.
    public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnStr);
        }
    }


}
