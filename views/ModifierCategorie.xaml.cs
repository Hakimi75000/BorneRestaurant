using BorneRestaurant.data;
using BorneRestaurant.models;
using MySql.Data.MySqlClient;
using System.Windows;

namespace BorneRestaurant.views
{
    public partial class ModifierCategorie : Window
    {
        private int idCategorie;

        public ModifierCategorie(Categorie cat)
        {
            InitializeComponent();

            idCategorie = cat.id;
            TxtNom.Text = cat.nom;
            TxtDescription.Text = cat.description;
        }

        private void BtnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtNom.Text))
            {
                MessageBox.Show("Le nom est obligatoire.");
                return;
            }

            using (var conn = Db.GetConnection())
            {
                conn.Open();
                string sql = "UPDATE categories SET nom=@nom, description=@desc WHERE id=@id;";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idCategorie);
                    cmd.Parameters.AddWithValue("@nom", TxtNom.Text.Trim());
                    cmd.Parameters.AddWithValue("@desc", TxtDescription.Text.Trim());
                    cmd.ExecuteNonQuery();
                }
            }

            DialogResult = true;
            Close();
        }
    }
}
