using BorneRestaurant.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using MySql.Data.MySqlClient;
using BorneRestaurant.data;

namespace BorneRestaurant.views
{
    public partial class AjoutProduit : Window
    {
        private int categorieId;

       
        public AjoutProduit(int categorieId)
        {
            InitializeComponent();
            this.categorieId = categorieId;
        }

        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtNom.Text))
            {
                MessageBox.Show("Le nom est obligatoire.");
                return;
            }

            string prixSaisi = TxtPrix.Text.Trim().Replace(',', '.');
            if (!decimal.TryParse(prixSaisi, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal prix))
            {
                MessageBox.Show("Prix invalide (ex: 9,90).");
                return;
            }

            using (var conn = Db.GetConnection())
            {
                conn.Open();

                string sql = @"INSERT INTO produits (nom, description, prix, categorie_id)
                               VALUES (@nom, @desc, @prix, @catId);";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nom", TxtNom.Text.Trim());
                    cmd.Parameters.AddWithValue("@desc", TxtDescription.Text.Trim());
                    cmd.Parameters.AddWithValue("@prix", prix);
                    cmd.Parameters.AddWithValue("@catId", categorieId);


                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Produit ajouté !");
            TxtNom.Clear();
            TxtPrix.Clear();
            TxtDescription.Clear();
            TxtNom.Focus();
        }
    }
}
