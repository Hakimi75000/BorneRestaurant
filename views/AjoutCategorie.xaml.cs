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
    public partial class AjoutCategorie : Window
    {
        public AjoutCategorie()
        {
            InitializeComponent();
        }

        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtNom.Text))
            {
                MessageBox.Show("Le nom est obligatoire.");
                return;
            }

            using (var conn = Db.GetConnection())
            {
                conn.Open();

                string sql = "INSERT INTO categories (nom, description) VALUES (@nom, @desc);";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nom", TxtNom.Text.Trim());
                    cmd.Parameters.AddWithValue("@desc", TxtDescription.Text.Trim());
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Catégorie ajoutée !");
            TxtNom.Clear();
            TxtDescription.Clear();
            TxtNom.Focus();
        }
    }
}

