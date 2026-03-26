using BorneRestaurant.data;
using BorneRestaurant.models;
using MySql.Data.MySqlClient;
using System.Globalization;
using System.Windows;

namespace BorneRestaurant.views
{
    public partial class ModifierProduit : Window
    {
        private int idProduit;

        public ModifierProduit(Produit p)
        {
            InitializeComponent();

            idProduit = p.id;
            TxtNom.Text = p.nom;
            TxtPrix.Text = p.prix.ToString(CultureInfo.InvariantCulture);
            TxtDescription.Text = p.description;
        }

        private void BtnEnregistrer_Click(object sender, RoutedEventArgs e)
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
                string sql = "UPDATE produits SET nom=@nom, description=@desc, prix=@prix WHERE id=@id;";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idProduit);
                    cmd.Parameters.AddWithValue("@nom", TxtNom.Text.Trim());
                    cmd.Parameters.AddWithValue("@desc", TxtDescription.Text.Trim());
                    cmd.Parameters.AddWithValue("@prix", prix);
                    cmd.ExecuteNonQuery();
                }
            }

            DialogResult = true;
            Close();
        }
    }
}
