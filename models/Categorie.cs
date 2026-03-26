using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BorneRestaurant.models
{
    public class Categorie
    {
        public int id { get; set; }                
        public string nom { get; set; }
        public string description { get; set; }    
        public ObservableCollection<Produit> produits { get; set; } = new ObservableCollection<Produit>();
    }
}
