using BorneRestaurant.data;
using BorneRestaurant.models;
using BorneRestaurant.views;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BorneRestaurant.views
{
    public partial class MenuWindow : Window
    {
        private ObservableCollection<Categorie> categories = new ObservableCollection<Categorie>();

        public MenuWindow(string mode)
        {
            InitializeComponent();
            TxtMode.Text = mode;

            ChargerCategoriesDepuisDb();
            ListCategories.ItemsSource = categories;

            if (categories.Count > 0)
                ListCategories.SelectedIndex = 0;
        }

        private void ChargerCategoriesDepuisDb()
        {
            categories.Clear();

            using (var conn = Db.GetConnection())
            {
                conn.Open();

                string sql = "SELECT id, nom, description FROM categories ORDER BY nom;";
                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new Categorie
                        {
                            id = reader.GetInt32("id"),
                            nom = reader.GetString("nom"),
                            description = reader.IsDBNull(reader.GetOrdinal("description")) ? "" : reader.GetString("description")
                        });
                    }
                }
            }
        }

        private void ChargerProduitsDepuisDb(int categorieId)
        {
            var produits = new ObservableCollection<Produit>();

            using (var conn = Db.GetConnection())
            {
                conn.Open();

                string sql = @"SELECT id, nom, description, prix, categorie_id
                               FROM produits
                               WHERE categorie_id = @id
                               ORDER BY nom;";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", categorieId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            produits.Add(new Produit
                            {
                                id = reader.GetInt32("id"),
                                nom = reader.GetString("nom"),
                                description = reader.IsDBNull(reader.GetOrdinal("description")) ? "" : reader.GetString("description"),
                                prix = reader.GetDecimal("prix"),
                                categorie_id = reader.GetInt32("categorie_id")
                            });
                        }
                    }
                }
            }

            GridProduits.ItemsSource = produits;
        }

        private void ListCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cat = ListCategories.SelectedItem as Categorie;
            if (cat == null) return;

            ChargerProduitsDepuisDb(cat.id);
        }

        private void BtnAjoutProduit_Click(object sender, RoutedEventArgs e)
        {
            var cat = ListCategories.SelectedItem as Categorie;
            if (cat == null)
            {
                MessageBox.Show("Choisis d'abord une catégorie.");
                return;
            }

            // ✅ on passe l'id de catégorie à la fenêtre d'ajout produit
            AjoutProduit ajoutProduit = new AjoutProduit(cat.id);
            ajoutProduit.ShowDialog();

            // ✅ refresh produits après ajout
            ChargerProduitsDepuisDb(cat.id);
        }

        // ✅ Ajout Catégorie (si tu as un bouton qui appelle cette fonction)
        private void BtnAjoutCategorie_Click(object sender, RoutedEventArgs e)
        {
            AjoutCategorie ajoutCategorie = new AjoutCategorie();
            ajoutCategorie.ShowDialog();

            // ✅ refresh catégories après ajout
            ChargerCategoriesDepuisDb();
            if (categories.Count > 0)
                ListCategories.SelectedIndex = 0;
        }

        private void BtnRetour_Click(object sender, RoutedEventArgs e)
        {
            ChoixWindow choix = new ChoixWindow();
            choix.Show();
            this.Close();
        }

        private void BtnSupprimerProduit_Click(object sender, RoutedEventArgs e)
        {
            var produit = GridProduits.SelectedItem as Produit;
            if (produit == null)
            {
                MessageBox.Show("Sélectionne un produit à supprimer.");
                return;
            }

            var confirm = MessageBox.Show(
                $"Supprimer '{produit.nom}' ?",
                "Confirmation",
                MessageBoxButton.YesNo);

            if (confirm != MessageBoxResult.Yes) return;

            using (var conn = Db.GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM produits WHERE id = @id;";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", produit.id);
                    cmd.ExecuteNonQuery();
                }
            }

            // Recharge la liste
            var cat = ListCategories.SelectedItem as Categorie;
            if (cat != null) ChargerProduitsDepuisDb(cat.id);
        }

        private void BtnSupprimerCategorie_Click(object sender, RoutedEventArgs e)
        {
            var cat = ListCategories.SelectedItem as Categorie;
            if (cat == null)
            {
                MessageBox.Show("Sélectionne une catégorie à supprimer.");
                return;
            }

            var confirm = MessageBox.Show(
                $"Supprimer la catégorie '{cat.nom}' ?",
                "Confirmation",
                MessageBoxButton.YesNo);

            if (confirm != MessageBoxResult.Yes) return;

            try
            {
                using (var conn = Db.GetConnection())
                {
                    conn.Open();
                    string sql = "DELETE FROM categories WHERE id = @id;";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", cat.id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException)
            {
                // Cas RESTRICT / clé étrangère
                MessageBox.Show("Impossible de supprimer : cette catégorie contient des produits.\nSupprime d'abord ses produits.");
                return;
            }

            ChargerCategoriesDepuisDb();
            GridProduits.ItemsSource = null;
        }

        private void BtnModifierCategorie_Click(object sender, RoutedEventArgs e)
        {
            var cat = ListCategories.SelectedItem as Categorie;
            if (cat == null)
            {
                MessageBox.Show("Sélectionne une catégorie.");
                return;
            }

            var w = new ModifierCategorie(cat);
            bool? ok = w.ShowDialog();

            if (ok == true)
            {
                ChargerCategoriesDepuisDb();
            }
        }

        private void BtnModifierProduit_Click(object sender, RoutedEventArgs e)
        {
            var p = GridProduits.SelectedItem as Produit;
            if (p == null)
            {
                MessageBox.Show("Sélectionne un produit.");
                return;
            }

            var w = new ModifierProduit(p);
            bool? ok = w.ShowDialog();

            if (ok == true)
            {
                var cat = ListCategories.SelectedItem as Categorie;
                if (cat != null) ChargerProduitsDepuisDb(cat.id);
            }
        }




    }
}

